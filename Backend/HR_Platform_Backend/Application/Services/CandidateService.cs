using Application.Helpers.Mappers;
using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateService(ICandidateRepository candidateRepository, ISkillRepository skillRepository)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
        }

        public async Task<ResultDTO> AddCandidateAsync(CandidateDTO candidate)
        {
            List<string> failedSkills = new List<string>();
            try
            {
                // check if email already exists
                if (await _candidateRepository.CandidateExistsByEmailAsync(candidate.Email))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Email already exists"
                    };
                }

                // check if contact number already exists
                if (await _candidateRepository.CandidateExistsByContactNumberAsync(candidate.ContactNumber))
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Contact number already exists"
                    };
                }

                // first we try to add the candidate only
                // if there's no errors -> then we add the skills
                var result = await _candidateRepository.AddCandidateAsync(CandidateMapper.DtoToEntity(candidate));

                // result here is the number of rows affected
                if (result.Id <= 0)
                {
                    return new ResultDTO
                    {
                        Success = false,
                        Message = "Failed to add candidate"
                    };
                }

                foreach (var skill in candidate.Skills)
                {
                    // if skill id is null -> it's a new skill
                    // that needs to be added to the database first
                    int skillId = skill.Id ?? -1;

                    if (skill.Id == null)
                    {
                        var addedSkill = await _skillRepository.AddSkillAsync(SkillMapper.DtoToEntity(skill));

                        if (addedSkill.Id <= 0)
                        {
                            failedSkills.Add(skill.Name);
                            continue;
                        }
                        skillId = addedSkill.Id;
                    }

                    var addedSkillToCandidate = await AddSkillToCandidateAsync(result.Id, skillId);
                    if (!addedSkillToCandidate.Success)
                    {
                        if (!failedSkills.Contains(skill.Name))
                        {
                            failedSkills.Add(skill.Name);
                        }
                        continue;
                    }
                }

                if (failedSkills.Any())
                {
                    return new ResultDTO
                    {
                        Success = true,
                        Message = $"Candidate added but failed to add the following skills: {string.Join(", ", failedSkills)}"
                    };
                }

                failedSkills.Clear();
                return new ResultDTO
                {
                    Success = true,
                    Message = "Candidate added successfully"
                };
            }
            catch (Exception ex)
            {
                failedSkills.Clear();
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Error adding candidate: {ex.Message}"
                };
            }
        }

        public async Task<ResultDTO> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            var skillResult = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillResult == null)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Skill with ID {skillId} does not exist"
                };
            }

            var addSkillResult = await _candidateRepository.AddSkillToCandidateAsync(candidateId, skillId);
            if (addSkillResult <= 0)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Failed to add skill with ID {skillId} to candidate"
                };
            }

            return new ResultDTO
            {
                Success = true,
                Message = "Skill added to candidate successfully"
            };
        }

        public async Task<ResultDTO> DeleteCandidateAsync(int candidateId)
        {
            var result = await _candidateRepository.DeleteCandidateAsync(new Candidate { Id = candidateId });
            if (result <= 0)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Failed to delete candidate"
                };
            }

            return new ResultDTO
            {
                Success = true,
                Message = "Candidate deleted successfully"
            };
        }

        public async Task<List<CandidateDTO>> GetAllCandidatesAsync()
        {
            var candidates = await _candidateRepository.GetAllCandidatesAsync();
            return candidates.Select(c => CandidateMapper.EntityToDto(c)).ToList();
        }

        public async Task<CandidateDTO?> GetCandidateByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetCandidateByIdAsync(id);
            if (candidate == null)
            {
                return null;
            }

            return CandidateMapper.EntityToDto(candidate);
        }

        public async Task<ResultDTO> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            var candidateResult = await _candidateRepository.GetCandidateByIdAsync(candidateId);
            if (candidateResult == null)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Candidate with ID {candidateId} does not exist"
                };
            }

            var skillResult = await _skillRepository.GetSkillByIdAsync(skillId);
            if (skillResult == null)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Skill with ID {skillId} does not exist"
                };
            }

            var removeSkillResult = await _candidateRepository.RemoveSkillFromCandidateAsync(candidateId, skillId);
            if (removeSkillResult <= 0)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Failed to remove skill with ID {skillId} from candidate"
                };
            }

            return new ResultDTO
            {
                Success = true,
                Message = "Skill removed from candidate successfully"
            };
        }

        public async Task<List<CandidateDTO>> SearchCandidatesAsync(string? name, List<string>? skills)
        {
            var candidates = await _candidateRepository.SearchCandidatesByNameAsync(name ?? "");

            return candidates.Where(c => skills == null || !skills.Any() 
            || skills.All(skill => c.CandidateSkills.Any(cs => cs.Skill.Name == skill)))
                .Select(c => CandidateMapper.EntityToDto(c)).ToList();
        }

        public async Task<ResultDTO> UpdateCandidateAsync(int candidateId, List<SkillDTO> skills)
        {
            var candidateResult = await _candidateRepository.GetCandidateByIdAsync(candidateId);
            if (candidateResult == null)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Candidate with ID {candidateId} does not exist"
                };
            }

            candidateResult.CandidateSkills.Clear();

            candidateResult.CandidateSkills = skills.Select(skill => new CandidateSkill
            {
                CandidateId = candidateId,
                SkillId = skill.Id!.Value
            }).ToList();

            
            var updateResult = await _candidateRepository.UpdateCandidateAsync(candidateResult);
            if (updateResult <= 0)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Failed to update candidate"
                };
            }

            return new ResultDTO
            {
                Success = true,
                Message = "Candidate updated successfully"
            };
        }
    }
}

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

namespace Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<ResultDTO> AddSkillAsync(SkillDTO skillDTO)
        {
            // check if skill already exists
            if (await _skillRepository.GetSkillByNameAsync(skillDTO.Name.Trim()) != null)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Skill already exists"
                };
            }
            var result = await _skillRepository.AddSkillAsync(SkillMapper.DtoToEntity(skillDTO));
            if (result.Id <= 0)
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = "Failed to add skill"
                };
            }
            return new ResultDTO
            {
                Success = true,
                Message = "Skill added successfully"
            };
        }

        public async Task<List<SkillDTO>> GetAllSkillsAsync()
        {
            var skills = await _skillRepository.GetAllSkillsAsync();
            return skills.Select(s => SkillMapper.EntityToDto(s)).ToList();
        }
    }
}

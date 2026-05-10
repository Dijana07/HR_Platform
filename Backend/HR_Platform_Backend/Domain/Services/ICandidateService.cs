using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICandidateService
    {
        Task<List<CandidateDTO>> GetAllCandidatesAsync();

        // NE TREBA
        Task<CandidateDTO?> GetCandidateByIdAsync(int id);
        Task<ResultDTO> AddCandidateAsync(CandidateDTO candidate);
        Task<ResultDTO> UpdateCandidateAsync(int candidateId, List<SkillDTO> skills);
        Task<ResultDTO> DeleteCandidateAsync(int candidateId);
        Task<List<CandidateDTO>> SearchCandidatesAsync(string? name, List<string>? skills);

        // ne trebaaa -> sve se radi u update
        Task<ResultDTO> AddSkillToCandidateAsync(int candidateId, int skillId);
        Task<ResultDTO> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}

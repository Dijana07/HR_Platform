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
        Task<ResultDTO> AddCandidateAsync(CandidateDTO candidate);
        Task<ResultDTO> UpdateCandidateAsync(int candidateId, List<SkillDTO> skills);
        Task<ResultDTO> DeleteCandidateAsync(int candidateId);
        Task<List<CandidateDTO>> SearchCandidatesAsync(string? name, List<string>? skills);
    }
}

using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetAllCandidatesAsync();
        Task<Candidate?> GetCandidateByIdAsync(int id);
        Task<List<Candidate>> SearchCandidatesByNameAsync(string name);
        Task<List<Candidate>> GetCandidateBySkillsAsync(List<string> skills);
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task<int> UpdateCandidateAsync(Candidate candidate);
        Task<int> DeleteCandidateAsync(Candidate candidate);
        Task<bool> CandidateExistsByEmailAsync(string email);
        Task<bool> CandidateExistsByContactNumberAsync(string contactNumber);

        Task<int> AddSkillToCandidateAsync(int candidateId, int skillId);
        Task<int> RemoveSkillFromCandidateAsync(int candidateId, int skillId);
    }
}

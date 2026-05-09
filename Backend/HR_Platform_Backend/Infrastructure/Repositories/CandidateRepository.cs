using Domain.Models;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext dbContext;

        public CandidateRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Candidate> AddCandidateAsync(Candidate candidate)
        {
            await dbContext.Candidates.AddAsync(candidate);
            await dbContext.SaveChangesAsync();

            return candidate;
        }

        public async Task<int> AddSkillToCandidateAsync(int candidateId, int skillId)
        {
            await dbContext.CandidateSkills.AddAsync(new CandidateSkill
            {
                CandidateId = candidateId,
                SkillId = skillId
            });
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CandidateExistsByContactNumberAsync(string contactNumber)
        {
            return await dbContext.Candidates.AnyAsync(c => c.ContactNumber == contactNumber);
        }

        public async Task<bool> CandidateExistsByEmailAsync(string email)
        {
            return await dbContext.Candidates.AnyAsync(c => c.Email == email);
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            return await dbContext.Candidates.Where(c => c.Id == candidate.Id).ExecuteDeleteAsync();
        }

        public async Task<Candidate?> GetCandidateByIdAsync(int id)
        {

            return await dbContext.Candidates
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Candidate>> GetCandidateBySkillsAsync(List<string> skills)
        {
            return await dbContext.Candidates
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .Where(c => c.CandidateSkills.Any(cs => skills.Contains(cs.Skill.Name)))
                .ToListAsync();
        }

        public async Task<int> RemoveSkillFromCandidateAsync(int candidateId, int skillId)
        {
            return await dbContext.CandidateSkills.Where(cs => cs.CandidateId == candidateId && cs.SkillId == skillId).ExecuteDeleteAsync();
        }

        public async Task<List<Candidate>> SearchCandidatesByNameAsync(string name)
        {
            return await dbContext.Candidates
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .Where(c => c.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            return await dbContext.Candidates.Where(c => c.Id == candidate.Id).ExecuteUpdateAsync(c => c
                .SetProperty(p => p.Name, candidate.Name)
                .SetProperty(p => p.DateOfBirth, candidate.DateOfBirth)
                .SetProperty(p => p.ContactNumber, candidate.ContactNumber)
                .SetProperty(p => p.Email, candidate.Email));
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            return await dbContext.Candidates
                .Include(c => c.CandidateSkills)
                .ThenInclude(cs => cs.Skill)
                .ToListAsync();
        }
    }
}

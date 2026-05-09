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
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext dbContext;

        public SkillRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            
            await dbContext.Skills.AddAsync(skill);
            await dbContext.SaveChangesAsync();

            return skill;
        }

        public async Task<int> DeleteSkillAsync(Skill skill)
        {

            return await dbContext.Skills.Where(s => s.Id == skill.Id).ExecuteDeleteAsync();
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            return await dbContext.Skills.ToListAsync();
        }

        public async Task<Skill?> GetSkillByIdAsync(int id)
        {
            return await dbContext.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill?> GetSkillByNameAsync(string name)
        {
            return await dbContext.Skills.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<int> UpdateSkillAsync(Skill skill)
        {
            return await dbContext.Skills.Where(s => s.Id == skill.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(sk => sk.Name, skill.Name));
        }
    }
}

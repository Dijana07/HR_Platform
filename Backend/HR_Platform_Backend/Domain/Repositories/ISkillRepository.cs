using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllSkillsAsync();
        Task<Skill?> GetSkillByIdAsync(int id);
        Task<Skill?> GetSkillByNameAsync(string name);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<int> UpdateSkillAsync(Skill skill);
        Task<int> DeleteSkillAsync(Skill skill);
    }
}

using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ISkillService
    {
        Task<SkillDTO?> GetSkillByIdAsync(int skillId);
        Task<SkillDTO?> GetSkillByNameAsync(string name);
        Task<List<SkillDTO>> GetAllSkillsAsync();
        Task<ResultDTO> AddSkillAsync(SkillDTO skill);
    }
}

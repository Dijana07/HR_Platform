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
        Task<List<SkillDTO>> GetAllSkillsAsync();
        Task<ResultDTO> AddSkillAsync(SkillDTO skill);
    }
}

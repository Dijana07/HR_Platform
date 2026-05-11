using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public static class SkillValidator
    {
        public static ResultDTO ValidateSkillDto(SkillDTO skill)
        {
            if (skill == null)
            {
                return new ResultDTO { Success = false, Message = "DTO not found." };
            }
            if (string.IsNullOrEmpty(skill.Name))
            {
                return new ResultDTO { Success = false, Message = "Skill name is required." };
            }

            return new ResultDTO { Success = true, Message = "Validation successful." };
        }

        public static ResultDTO ValidateSkills(List<SkillDTO> skills)
        {
            if (skills.Any(x => x.Id == null || x.Id <= 0))
            {
                return new ResultDTO { Success = false, Message = "All skills must have valid IDs." };
            }

            return new ResultDTO { Success = true, Message = "Validation successful." };
        }
    }
}

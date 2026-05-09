using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Mappers
{
    public static class SkillMapper
    {
        public static Skill DtoToEntity(SkillDTO dto)
        {
            return new Skill
            {
                Id = dto.Id ?? 0,
                Name = dto.Name
            };
        }

        public static SkillDTO EntityToDto(Skill entity)
        {
            return new SkillDTO
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}

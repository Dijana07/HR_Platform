using Domain.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.Mappers
{
    public static class CandidateMapper
    {
        public static Candidate DtoToEntity(CandidateDTO dto)
        {
            return new Candidate
            {
                Id = dto.Id ?? 0,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber
            };
        }

        public static CandidateDTO EntityToDto(Candidate entity)
        {
            return new CandidateDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Email = entity.Email,
                ContactNumber = entity.ContactNumber,
                Skills = entity.CandidateSkills?.Select(cs => new SkillDTO
                {
                    Id = cs.SkillId,
                    Name = cs.Skill?.Name ?? string.Empty
                }).ToList() ?? new List<SkillDTO>()
            };
        }
    }
}

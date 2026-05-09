using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CandidateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; } = new DateOnly();
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<SkillDTO> Skills { get; set; } = new List<SkillDTO>();
        public CandidateDTO() { }
        public CandidateDTO(int? id, string name, DateOnly dob, string contactNumber, string email, List<SkillDTO> skills)
        {
            Id = id;
            Name = name;
            DateOfBirth = dob;
            ContactNumber = contactNumber;
            Email = email;
            Skills = skills;
        }
    }
}

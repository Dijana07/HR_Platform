using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();

        public Candidate() { }

        public Candidate(string name, DateOnly dateOfBirth, string contactNumber, string email)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            ContactNumber = contactNumber;
            Email = email;
        }
    }
}

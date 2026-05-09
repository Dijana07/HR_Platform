using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();

        public Skill() { }

        public Skill(string name)
        {
            Name = name;
        }
    }
}

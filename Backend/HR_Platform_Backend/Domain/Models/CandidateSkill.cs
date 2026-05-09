using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
        public CandidateSkill() { }
        public CandidateSkill(int candidateId, Candidate candidate, int skillId, Skill skill)
        {
            CandidateId = candidateId;
            Candidate = candidate;
            SkillId = skillId;
            Skill = skill;
        }
    }
}

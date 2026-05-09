using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class SkillDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SkillDTO() { }
        public SkillDTO(int? id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "C# programming" },
                new Skill { Id = 2, Name = "Java programming" },
                new Skill { Id = 3, Name = "Python" },
                new Skill { Id = 4, Name = "JavaScript" },
                new Skill { Id = 5, Name = "SQL" },
                new Skill { Id = 6, Name = "Database design" },
                new Skill { Id = 7, Name = "NoSQL databases" },
                new Skill { Id = 8, Name = "English" },
                new Skill { Id = 9, Name = "German" },
                new Skill { Id = 10, Name = "Russian" },
                new Skill { Id = 11, Name = "React" },
                new Skill { Id = 12, Name = ".NET" },
                new Skill { Id = 13, Name = "C programming" },
                new Skill { Id = 14, Name = "C++ programming" },
                new Skill { Id = 15, Name = "DevOps" },
                new Skill { Id = 16, Name = "PHP" },
                new Skill { Id = 17, Name = "Jira" },
                new Skill { Id = 18, Name = "Git" },
                new Skill { Id = 19, Name = "Docker" },
                new Skill { Id = 20, Name = "AWS" }
            );

            modelBuilder.Entity<Candidate>().HasData(
                new Candidate { Id = 1, Name = "Anne Smith", DateOfBirth = new DateOnly(2000, 1, 1), 
                    ContactNumber = "+381601234567", Email = "anne.smith@gmail.com" },
                new Candidate { Id = 2, Name = "John Peterson", DateOfBirth = new DateOnly(1998, 3, 15), 
                    ContactNumber = "+381601234568", Email = "john.peterson@gmail.com" },
                new Candidate { Id = 3, Name = "Emily Johnson", DateOfBirth = new DateOnly(1997, 7, 22), 
                    ContactNumber = "+381601234569", Email = "emily.johnson@gmail.com" },
                new Candidate { Id = 4, Name = "Michael Brown", DateOfBirth = new DateOnly(1995, 11, 5), 
                    ContactNumber = "+381601234570", Email = "michael.brown@gmail.com" },
                new Candidate { Id = 5, Name = "Sophia Davis", DateOfBirth = new DateOnly(2001, 2, 10), 
                    ContactNumber = "+381601234571", Email = "sophia.davis@gmail.com" },
                new Candidate { Id = 6, Name = "Daniel Wilson", DateOfBirth = new DateOnly(1996, 9, 18), 
                    ContactNumber = "+381601234572", Email = "daniel.wilson@gmail.com" },
                new Candidate { Id = 7, Name = "Olivia Martinez", DateOfBirth = new DateOnly(1999, 12, 3), 
                    ContactNumber = "+381601234573", Email = "olivia.martinez@gmail.com" },
                new Candidate { Id = 8, Name = "James Anderson", DateOfBirth = new DateOnly(1994, 6, 27), 
                    ContactNumber = "+381601234574", Email = "james.anderson@gmail.com" },
                new Candidate { Id = 9, Name = "Emma Thomas", DateOfBirth = new DateOnly(2002, 4, 14), 
                    ContactNumber = "+381601234575", Email = "emma.thomas@gmail.com" },
                new Candidate { Id = 10, Name = "William Taylor", DateOfBirth = new DateOnly(1993, 8, 30), 
                    ContactNumber = "+381601234576", Email = "william.taylor@gmail.com" }
            );

            modelBuilder.Entity<CandidateSkill>().HasData(
                // Anne Smith : C#, SQL, .NET, Git
                new CandidateSkill { CandidateId = 1, SkillId = 1 },   
                new CandidateSkill { CandidateId = 1, SkillId = 5 },  
                new CandidateSkill { CandidateId = 1, SkillId = 12 }, 
                new CandidateSkill { CandidateId = 1, SkillId = 18 },  

                // John Peterson : Java, SQL, Jira, AWS
                new CandidateSkill { CandidateId = 2, SkillId = 2 },   
                new CandidateSkill { CandidateId = 2, SkillId = 5 },   
                new CandidateSkill { CandidateId = 2, SkillId = 17 },  
                new CandidateSkill { CandidateId = 2, SkillId = 20 },

                // Emily Johnson : Python, NoSQL, Docker
                new CandidateSkill { CandidateId = 3, SkillId = 3 }, 
                new CandidateSkill { CandidateId = 3, SkillId = 7 }, 
                new CandidateSkill { CandidateId = 3, SkillId = 19 }, 

                // Michael Brown : JavaScript, React, Git
                new CandidateSkill { CandidateId = 4, SkillId = 4 }, 
                new CandidateSkill { CandidateId = 4, SkillId = 11 },
                new CandidateSkill { CandidateId = 4, SkillId = 18 }, 

                // Sophia Davis : C#, Database design, .NET
                new CandidateSkill { CandidateId = 5, SkillId = 1 },
                new CandidateSkill { CandidateId = 5, SkillId = 6 },
                new CandidateSkill { CandidateId = 5, SkillId = 12 },  

                // Daniel Wilson : C, C++, Git
                new CandidateSkill { CandidateId = 6, SkillId = 13 }, 
                new CandidateSkill { CandidateId = 6, SkillId = 14 }, 
                new CandidateSkill { CandidateId = 6, SkillId = 18 },  

                // Olivia Martinez : English, German, Jira
                new CandidateSkill { CandidateId = 7, SkillId = 8 },  
                new CandidateSkill { CandidateId = 7, SkillId = 9 },  
                new CandidateSkill { CandidateId = 7, SkillId = 17 }, 

                // James Anderson : DevOps, Docker, AWS
                new CandidateSkill { CandidateId = 8, SkillId = 15 }, 
                new CandidateSkill { CandidateId = 8, SkillId = 19 }, 
                new CandidateSkill { CandidateId = 8, SkillId = 20 }, 

                // Emma Thomas : PHP, SQL, Git
                new CandidateSkill { CandidateId = 9, SkillId = 16 }, 
                new CandidateSkill { CandidateId = 9, SkillId = 5 }, 
                new CandidateSkill { CandidateId = 9, SkillId = 18 },  

                // William Taylor : C#, React, .NET, Docker
                new CandidateSkill { CandidateId = 10, SkillId = 1 }, 
                new CandidateSkill { CandidateId = 10, SkillId = 11 }, 
                new CandidateSkill { CandidateId = 10, SkillId = 12 }, 
                new CandidateSkill { CandidateId = 10, SkillId = 19 }  
            );  
        }
    }
}

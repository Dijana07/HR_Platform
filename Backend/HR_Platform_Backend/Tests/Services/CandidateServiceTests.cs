using Application.Services;
using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    [TestFixture]
    public class CandidateServiceTests
    {
        private ICandidateService candidateService;
        private Mock<ICandidateRepository> candidateRepository;
        private Mock<ISkillRepository> skillRepository;


        [SetUp]
        public void SetUp()
        {
            candidateRepository = new Mock<ICandidateRepository>();
            skillRepository = new Mock<ISkillRepository>();
            candidateService = new CandidateService(candidateRepository.Object, skillRepository.Object);
        }

        #region GetAllCandidates
        [Test]
        public async Task GetAllCandidatesAsync_ShouldReturnCandidates()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Anne Smith", DateOfBirth = new DateOnly(2000, 1, 1),
                    ContactNumber = "+381601234567", Email = "anne.smith@gmail.com" },
                new Candidate { Id = 2, Name = "John Peterson", DateOfBirth = new DateOnly(1998, 3, 15),
                    ContactNumber = "+381601234568", Email = "john.peterson@gmail.com" },
                new Candidate { Id = 3, Name = "Emily Johnson", DateOfBirth = new DateOnly(1997, 7, 22),
                    ContactNumber = "+381601234569", Email = "emily.johnson@gmail.com" },
                new Candidate { Id = 4, Name = "Michael Brown", DateOfBirth = new DateOnly(1995, 11, 5),
                    ContactNumber = "+381601234570", Email = "michael.brown@gmail.com" }
            };

            candidateRepository
                .Setup(x => x.GetAllCandidatesAsync())
                .ReturnsAsync(candidates);

            var result = await candidateService.GetAllCandidatesAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(4));
        }
        #endregion

        #region AddCandidate
        [Test]
        public async Task AddCandidateAsync_ShouldAddCandidateWithNoSkills()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com"
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.CandidateExistsByContactNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(new Candidate
                {
                    Id = 1
                });

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Candidate added successfully"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldAddCandidateWithSkills()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com",
                Skills = new List<SkillDTO>
                {
                    new SkillDTO
                    {
                        Name = "React"
                    }
                }
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.CandidateExistsByContactNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(new Candidate
                {
                    Id = 1
                });

            skillRepository
                .Setup(x => x.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(new Skill
                {
                    Id = 1
                });

            skillRepository
                .Setup(x => x.GetSkillByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Skill
                {
                    Id = 1,
                    Name = "React"
                });

            candidateRepository
                .Setup(x => x.AddSkillToCandidateAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(1);

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Candidate added successfully"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldNotAddCandidate_WhenEmailAlreadyExists()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com"
            };

            candidateRepository
               .Setup(x => x.CandidateExistsByEmailAsync(candidate.Email))
               .ReturnsAsync(true);

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Email already exists"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldNotAddCandidate_WhenContactNumberAlreadyExists()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com"
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
               .Setup(x => x.CandidateExistsByContactNumberAsync(candidate.ContactNumber))
               .ReturnsAsync(true);

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Contact number already exists"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldNotAddCandidate_WhenRepositoryDoesntAdd()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com"
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
               .Setup(x => x.CandidateExistsByContactNumberAsync(candidate.ContactNumber))
               .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(new Candidate
                {
                    Id = 0
                });

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Failed to add candidate"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldReturnSuccessWithFailedSkillsMessage()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com",
                Skills = new List<SkillDTO>
                {
                    new SkillDTO
                    {
                        Name = "React"
                    }
                }
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.CandidateExistsByContactNumberAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            candidateRepository
                .Setup(x => x.AddCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(new Candidate
                {
                    Id = 1
                });

            skillRepository
                .Setup(x => x.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(new Skill
                {
                    Id = 0
                });

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Candidate added but failed to add the following skills: React"));
        }

        [Test]
        public async Task AddCandidateAsync_ShouldNotAddCandidate_WhenCatchesException()
        {
            var candidate = new CandidateDTO
            {
                Name = "Anne Smith",
                DateOfBirth = new DateOnly(2000, 1, 1),
                ContactNumber = "+381601234567",
                Email = "anne.smith@gmail.com"
            };

            candidateRepository
                .Setup(x => x.CandidateExistsByEmailAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Database error"));

            var result = await candidateService.AddCandidateAsync(candidate);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Error adding candidate: Database error"));
        }
        #endregion

        #region DeleteCandidate
        [Test]
        public async Task DeleteCandidateAsync_ShouldDeleteCandidate()
        {
            candidateRepository
                .Setup(x => x.DeleteCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(1);

            var result = await candidateService.DeleteCandidateAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Candidate deleted successfully"));
        }

        [Test]
        public async Task DeleteCandidateAsync_ShouldNotDeleteCandidate_WhenRepositoryDoesntDelete()
        {
            candidateRepository
                .Setup(x => x.DeleteCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(0);

            var result = await candidateService.DeleteCandidateAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Failed to delete candidate"));
        }
        #endregion

        #region SearchCandidates
        [Test]
        public async Task SearchCandidatesAsync_ShouldReturnAllCandidates()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Anne Smith", DateOfBirth = new DateOnly(2000, 1, 1),
                    ContactNumber = "+381601234567", Email = "anne.smith@gmail.com" },
                new Candidate { Id = 2, Name = "John Peterson", DateOfBirth = new DateOnly(1998, 3, 15),
                    ContactNumber = "+381601234568", Email = "john.peterson@gmail.com" },
                new Candidate { Id = 3, Name = "Emily Johnson", DateOfBirth = new DateOnly(1997, 7, 22),
                    ContactNumber = "+381601234569", Email = "emily.johnson@gmail.com" },
                new Candidate { Id = 4, Name = "Michael Brown", DateOfBirth = new DateOnly(1995, 11, 5),
                    ContactNumber = "+381601234570", Email = "michael.brown@gmail.com" }
            };

            candidateRepository
                .Setup(x => x.SearchCandidatesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(candidates);

            var result = await candidateService.SearchCandidatesAsync("", null);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result[0].Name, Is.EqualTo("Anne Smith"));
        }

        [Test]
        public async Task SearchCandidatesAsync_ShouldReturnCandidatesByNameOnly()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Anne Smith", DateOfBirth = new DateOnly(2000, 1, 1),
                    ContactNumber = "+381601234567", Email = "anne.smith@gmail.com" },
                new Candidate { Id = 2, Name = "John Peterson", DateOfBirth = new DateOnly(1998, 3, 15),
                    ContactNumber = "+381601234568", Email = "john.peterson@gmail.com" },
                new Candidate { Id = 3, Name = "Emily Johnson", DateOfBirth = new DateOnly(1997, 7, 22),
                    ContactNumber = "+381601234569", Email = "emily.johnson@gmail.com" },
                new Candidate { Id = 4, Name = "Michael Brown", DateOfBirth = new DateOnly(1995, 11, 5),
                    ContactNumber = "+381601234570", Email = "michael.brown@gmail.com" }
            };

            candidateRepository
                .Setup(x => x.SearchCandidatesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(candidates.Where(x =>
                    x.Name.Contains("Anne")).ToList());

            var result = await candidateService.SearchCandidatesAsync("an", null);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Anne Smith"));
        }

        [Test]
        public async Task SearchCandidatesAsync_ShouldReturnEmptyList_WhenNoCandidatesFound()
        {
            candidateRepository
                .Setup(x => x.SearchCandidatesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Candidate>());

            var result = await candidateService.SearchCandidatesAsync("pe", null);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task SearchCandidatesAsync_ShouldFilterCandidatesBySkills()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Anne Smith", CandidateSkills = new List<CandidateSkill>
                    { new CandidateSkill { Skill = new Skill { Name = "React" }}}},
                new Candidate { Id = 2, Name = "John Peterson", CandidateSkills = new List<CandidateSkill>
                    { new CandidateSkill { Skill = new Skill { Name = "Angular" }}}}
            };

            candidateRepository
                .Setup(x => x.SearchCandidatesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(candidates);

            var result = await candidateService.SearchCandidatesAsync("", new List<string> { "React" });

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Anne Smith"));
        }

        [Test]
        public async Task SearchCandidatesAsync_ShouldReturnCandidatesWithAllSkills()
        {
            var candidates = new List<Candidate>
            {
                new Candidate { Id = 1, Name = "Anne Smith", CandidateSkills = new List<CandidateSkill>
                    { new CandidateSkill { Skill = new Skill { Name = "React" }},
                      new CandidateSkill { Skill = new Skill { Name = "C#" }}}},
                new Candidate { Id = 2, Name = "John Peterson", CandidateSkills = new List<CandidateSkill>
                    { new CandidateSkill { Skill = new Skill { Name = "React" }}}},
            };

            candidateRepository
                .Setup(x => x.SearchCandidatesByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(candidates);

            var result = await candidateService.SearchCandidatesAsync("", new List<string> { "React", "C#" });

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Anne Smith"));
        }
        #endregion

        #region UpdateCandidate
        [Test]
        public async Task UpdateCandidateAsync_ShouldReturnFalse_WhenCandidateDoesNotExist()
        {
            candidateRepository
                .Setup(x => x.GetCandidateByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Candidate)null);

            var result = await candidateService.UpdateCandidateAsync(1, new List<SkillDTO>());

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Candidate with ID 1 does not exist"));
        }

        [Test]
        public async Task UpdateCandidateAsync_ShouldReturnFalse_WhenRepositoryDoesntUpdate()
        {
            var candidate = new Candidate { Id = 1, CandidateSkills = new List<CandidateSkill>() };

            candidateRepository
                .Setup(x => x.GetCandidateByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(candidate);

            candidateRepository
                .Setup(x => x.UpdateCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(0);

            var result = await candidateService.UpdateCandidateAsync(1, new List<SkillDTO> { new SkillDTO { Id = 1, Name = "React" } });

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Failed to update candidate"));
        }

        [Test]
        public async Task UpdateCandidateAsync_ShouldUpdateCandidate()
        {
            var candidate = new Candidate { Id = 1, CandidateSkills = new List<CandidateSkill>() };

            candidateRepository
                .Setup(x => x.GetCandidateByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(candidate);

            candidateRepository
                .Setup(x => x.UpdateCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(1);

            var skills = new List<SkillDTO> { new SkillDTO { Id = 1, Name = "React" }, new SkillDTO { Id = 2, Name = "C#" } };

            var result = await candidateService.UpdateCandidateAsync(1, skills);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Candidate updated successfully"));
        }

        [Test]
        public async Task UpdateCandidateAsync_ShouldReplaceCandidateSkills()
        {
            var candidate = new Candidate { Id = 1, CandidateSkills = new List<CandidateSkill> { new CandidateSkill { SkillId = 99 } } };

            candidateRepository
                .Setup(x => x.GetCandidateByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(candidate);

            candidateRepository
                .Setup(x => x.UpdateCandidateAsync(It.IsAny<Candidate>()))
                .ReturnsAsync(1);

            var skills = new List<SkillDTO> { new SkillDTO { Id = 1, Name = "React" } };

            await candidateService.UpdateCandidateAsync(1, skills);

            Assert.That(candidate.CandidateSkills.Count, Is.EqualTo(1));
            Assert.That(candidate.CandidateSkills.First().SkillId, Is.EqualTo(1));
        }
        #endregion
    }
}

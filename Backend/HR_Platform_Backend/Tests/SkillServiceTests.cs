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

namespace Tests
{
    [TestFixture]
    public class SkillServiceTests
    {
        private ISkillService skillService;
        private Mock<ISkillRepository> skillRepository;

        [SetUp]
        public void SetUp()
        {
            skillRepository = new Mock<ISkillRepository>();
            skillService = new SkillService(skillRepository.Object);
        }

        #region GetAllSkills
        [Test]
        public async Task GetAllSkillsAsync_ShouldReturnSkills()
        {
            var skills = new List<Skill>
            {
                new Skill { Id = 1, Name = "C# programming" },
                new Skill { Id = 2, Name = "Java programming" },
                new Skill { Id = 3, Name = "Python" },
                new Skill { Id = 4, Name = "JavaScript" },
                new Skill { Id = 5, Name = "SQL" }
            };

            skillRepository
                .Setup(x => x.GetAllSkillsAsync())
                .ReturnsAsync(skills);

            var result = await skillService.GetAllSkillsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(5));
        }
        #endregion

        #region AddSkill
        [Test]
        public async Task AddSkillAsync_ShouldAddSkill()
        {
            var skill = new SkillDTO
            {
                Name = "Python"
            };

            skillRepository
                .Setup(x => x.GetSkillByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Skill)null);

            skillRepository
                .Setup(x => x.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(new Skill
                {
                    Id = 1
                });

            var result = await skillService.AddSkillAsync(skill);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Skill added successfully"));
        }

        [Test]
        public async Task AddSkillAsync_ShouldNotAddSkill_WhenSkillAlreadyExists()
        {
            var skill = new SkillDTO
            {
                Name = "Python"
            };

            skillRepository
                .Setup(x => x.GetSkillByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new Skill{ Name = "Python" });

            var result = await skillService.AddSkillAsync(skill);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Skill already exists"));
        }

        [Test]
        public async Task AddSkillAsync_ShouldNotAddSkill_WhenRepositoryDoesntAdd()
        {
            var skill = new SkillDTO
            {
                Name = "Python"
            };

            skillRepository
                .Setup(x => x.GetSkillByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((Skill)null);

            skillRepository
                .Setup(x => x.AddSkillAsync(It.IsAny<Skill>()))
                .ReturnsAsync(new Skill
                {
                    Id = 0
                });

            var result = await skillService.AddSkillAsync(skill);

            Assert.IsNotNull(result);
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Failed to add skill"));
        }
        #endregion
    }
}

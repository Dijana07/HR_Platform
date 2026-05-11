using Application.Validators;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tests.Validators
{
    [TestFixture]
    public class SkillValidatorTests
    {
        [Test]
        public void ValidateSkillDto_ShouldReturnFalse_WhenDtoIsNull()
        {
            var result = SkillValidator.ValidateSkillDto(null);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("DTO not found."));
        }

        [Test]
        [TestCase(null, false, "Skill name is required.")]
        [TestCase("", false, "Skill name is required.")]
        public void ValidateSkillDto_ShouldReturnFalse_WhenNameIsNullOrEmpty(string? name, bool expectedSuccess, string expectedMessage)
        {
            var dto = new SkillDTO { Name = name };
            var result = SkillValidator.ValidateSkillDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase("React", true, "Validation successful.")]
        public void ValidateSkillDto_ShouldReturnTrue_WhenSkillIsValid(string name, bool expectedSuccess, string expectedMessage)
        {
            var dto = new SkillDTO { Name = name };
            var result = SkillValidator.ValidateSkillDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase("React", 1, true, "Validation successful.")]
        public void ValidateSkillDto_ShouldReturnTrue_WhenSkillsAreValid(string name, int id, bool expectedSuccess, string expectedMessage)
        {
            var skills = new List<SkillDTO> { new SkillDTO { Name = name, Id =  id} };
            var result = SkillValidator.ValidateSkills(skills);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase(null, false, "All skills must have valid IDs.")]
        public void ValidateSkillDto_ShouldReturnFalse_WhenSkillsIdsAreInvalid(int? id, bool expectedSuccess, string expectedMessage)
        {
            var skills = new List<SkillDTO> { new SkillDTO { Name = "React", Id = id } };
            var result = SkillValidator.ValidateSkills(skills);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }
    }
}

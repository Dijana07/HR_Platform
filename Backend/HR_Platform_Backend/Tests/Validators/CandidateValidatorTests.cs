using Application.Validators;
using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Validators
{
    [TestFixture]
    public class CandidateValidatorTests
    {
        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenDtoIsNull()
        {
            var result = CandidateValidator.ValidateCandidateDto(null);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("DTO not found."));
        }

        [Test]
        [TestCase(" ", false, "Contact number can contain at least 6 numbers and optional '+'")]
        [TestCase("aaa", false, "Contact number can contain at least 6 numbers and optional '+'")]
        [TestCase("060", false, "Contact number can contain at least 6 numbers and optional '+'")]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenContactNumberFormatIsNotValid(string contact, bool expectedSuccess, string expectedMessage)
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = contact, DateOfBirth = new DateOnly(2000, 1, 1) };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenDateOfBirthIsInTheFuture()
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = new DateOnly(2030, 12, 1) };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(false));
            Assert.That(result.Message, Is.EqualTo("Date of birth cannot be in the future"));
        }

        [Test]
        [TestCase(null, false, "Missing required field(s): name")]
        [TestCase("", false, "Missing required field(s): name")]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenNameIsNullOrEmpty(string? name, bool expectedSuccess, string expectedMessage)
        {
            var dto = new CandidateDTO { Name = name, Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = new DateOnly(2000, 1, 1) };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase(null, false, "Missing required field(s): email")]
        [TestCase("", false, "Missing required field(s): email")]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenEmailIsNullOrEmpty(string? email, bool expectedSuccess, string expectedMessage)
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = email, ContactNumber = "+111111111", DateOfBirth = new DateOnly(2000, 1, 1) };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        [TestCase(null, false, "Missing required field(s): contact number")]
        [TestCase("", false, "Missing required field(s): contact number")]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenContactNumberIsNullOrEmpty(string? contact, bool expectedSuccess, string expectedMessage)
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = contact, DateOfBirth = new DateOnly(2000, 1, 1) };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(expectedSuccess));
            Assert.That(result.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenDateOfBirthIsDEfault()
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = default };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.EqualTo(false));
            Assert.That(result.Message, Is.EqualTo("Missing required field(s): date of birth"));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenCandidateFieldsAreMissing()
        {
            var dto = new CandidateDTO();
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Missing required field(s): name, email, contact number, date of birth"));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenSkillIdIsInvalid()
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = new DateOnly(2000, 1, 1),
                Skills = new List<SkillDTO> { new SkillDTO { Id = 0, Name = "React" }}};
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Invalid skill data: invalid skill id"));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnFalse_WhenNewSkillNameIsMissing()
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = new DateOnly(2000, 1, 1),
                Skills = new List<SkillDTO> { new SkillDTO { Name = "" }}};
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Invalid skill data: skill name"));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnTrue_WhenCandidateIsValidWithSkills()
        {
            var dto = new CandidateDTO { Name = "John Doe", Email = "someone@mail.com", ContactNumber = "+111111111", DateOfBirth = new DateOnly(2000, 1, 1),
                Skills = new List<SkillDTO> { new SkillDTO { Id = 1, Name = "React" }}};
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Validation successful."));
        }

        [Test]
        public void ValidateCandidateDto_ShouldReturnTrue_WhenCandidateIsValidWithoutSkills()
        {
            var dto = new CandidateDTO
            {
                Name = "John Doe",
                Email = "someone@mail.com",
                ContactNumber = "+111111111",
                DateOfBirth = new DateOnly(2000, 1, 1)
            };
            var result = CandidateValidator.ValidateCandidateDto(dto);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Validation successful."));
        }
    }
}

using Application.Validators;
using Domain.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var skills = await _skillService.GetAllSkillsAsync();
            return Ok(skills);
        }


        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] SkillDTO skill)
        {
            var valid = SkillValidator.ValidateSkillDto(skill);
            if (!valid.Success)
            {
                return BadRequest(valid.Message);
            }

            var result = await _skillService.AddSkillAsync(skill);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}

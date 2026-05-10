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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var skill = await _skillService.GetSkillByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSkillByName([FromQuery] string name)
        {
            var skill = await _skillService.GetSkillByNameAsync(name);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill([FromBody] SkillDTO skill)
        {
            var result = await _skillService.AddSkillAsync(skill);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}

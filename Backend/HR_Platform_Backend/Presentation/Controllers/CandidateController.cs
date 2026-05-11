using Application.Validators;
using Domain.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateDTO candidate)
        {
            var isValid = CandidateValidator.ValidateCandidateDto(candidate);
            if (!isValid.Success)
            {
                return BadRequest(isValid);
            }

            var result = await _candidateService.AddCandidateAsync(candidate);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] List<SkillDTO> skills)
        {
            var isValid = SkillValidator.ValidateSkills(skills);
            if (!isValid.Success)
            {
                return BadRequest(isValid);
            }

            var result = await _candidateService.UpdateCandidateAsync(id, skills);
            if (!result.Success)
            { 
                return BadRequest(result); 
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _candidateService.DeleteCandidateAsync(id);
            if (!result.Success)
            { 
                return BadRequest(result); 
            }
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchCandidates([FromQuery] string? name, [FromQuery] List<string>? skills)
        {
            var candidates = await _candidateService.SearchCandidatesAsync(name, skills);
            return Ok(candidates);
        }
    }
}

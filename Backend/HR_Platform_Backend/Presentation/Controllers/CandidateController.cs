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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCandidateById(int id)
        {
            var candidate = await _candidateService.GetCandidateByIdAsync(id);
            if (candidate == null) return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] CandidateDTO candidate)
        {
            var result = await _candidateService.AddCandidateAsync(candidate);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCandidate(int id, [FromBody] CandidateDTO candidate)
        {
            var result = await _candidateService.UpdateCandidateAsync(id, candidate);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var result = await _candidateService.DeleteCandidateAsync(id);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpPost("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> AddSkillToCandidate(int candidateId, int skillId)
        {
            var result = await _candidateService.AddSkillToCandidateAsync(candidateId, skillId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }

        [HttpDelete("{candidateId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkillFromCandidate(int candidateId, int skillId)
        {
            var result = await _candidateService.RemoveSkillFromCandidateAsync(candidateId, skillId);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Message);
        }
    }
}

using FootballPrediction.Application.DTOs.Matches;
using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/matches")]
[Authorize(Roles = "Admin")]
public class MatchAdminController : ControllerBase
{
    private readonly IMatchService _matches;

    public MatchAdminController(IMatchService matches) => _matches = matches;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMatchRequest request)
    {
        try
        {
            var result = await _matches.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try { return Ok(await _matches.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMatchRequest request)
    {
        try { return Ok(await _matches.UpdateAsync(id, request)); }
        catch (KeyNotFoundException) { return NotFound(); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
    }

    [HttpPut("{id}/result")]
    public async Task<IActionResult> EnterResult(Guid id, [FromBody] EnterResultRequest request)
    {
        try { return Ok(await _matches.EnterResultAsync(id, request)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPost("gameweek/{gameWeekId}/import")]
    public async Task<IActionResult> Import(Guid gameWeekId)
    {
        try
        {
            var count = await _matches.ImportFromApiAsync(gameWeekId);
            return Ok(new { imported = count });
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (Exception ex) { return BadRequest(new { message = ex.Message }); }
    }
}

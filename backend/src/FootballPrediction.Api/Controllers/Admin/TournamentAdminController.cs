using FootballPrediction.Application.DTOs.Tournaments;
using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers.Admin;

[ApiController]
[Route("api/v1/admin/tournaments")]
[Authorize(Roles = "Admin")]
public class TournamentAdminController : ControllerBase
{
    private readonly ITournamentService _tournaments;

    public TournamentAdminController(ITournamentService tournaments) => _tournaments = tournaments;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _tournaments.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try { return Ok(await _tournaments.GetByIdAsync(id)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTournamentRequest request)
    {
        var result = await _tournaments.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTournamentRequest request)
    {
        try { return Ok(await _tournaments.UpdateAsync(id, request)); }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try { await _tournaments.DeleteAsync(id); return NoContent(); }
        catch (KeyNotFoundException) { return NotFound(); }
    }
}

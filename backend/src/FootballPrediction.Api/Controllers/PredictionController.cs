using System.Security.Claims;
using FootballPrediction.Application.DTOs.Predictions;
using FootballPrediction.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballPrediction.Api.Controllers;

[ApiController]
[Route("api/v1/predictions")]
[Authorize]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictions;

    public PredictionController(IPredictionService predictions) => _predictions = predictions;

    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var userId = GetUserId();
        var preds = await _predictions.GetMyPredictionsAsync(userId);
        return Ok(preds);
    }

    [HttpGet("match/{matchId}")]
    public async Task<IActionResult> GetByMatch(Guid matchId)
    {
        var userId = GetUserId();
        try
        {
            var preds = await _predictions.GetMatchPredictionsAsync(matchId, userId);
            return Ok(preds);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Submit([FromBody] SubmitPredictionRequest request)
    {
        var userId = GetUserId();
        try
        {
            var pred = await _predictions.SubmitAsync(userId, request);
            return CreatedAtAction(nameof(GetMy), pred);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePredictionRequest request)
    {
        var userId = GetUserId();
        try
        {
            var pred = await _predictions.UpdateAsync(id, userId, request);
            return Ok(pred);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}

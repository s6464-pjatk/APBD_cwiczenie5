using Cwiczenia5.Dtos;
using Cwiczenia5.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia5.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController : ControllerBase
{
    private readonly IPcService _pcService;

    public PcsController(IPcService pcService)
    {
        _pcService = pcService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PcResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PcResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        var pcs = await _pcService.GetAllAsync(cancellationToken);
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    [ProducesResponseType(typeof(PcWithComponentsResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PcWithComponentsResponseDto>> GetByIdComponents(int id, CancellationToken cancellationToken)
    {
        var pc = await _pcService.GetByIdWithComponentsAsync(id, cancellationToken);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PcResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PcResponseDto>> Create(PcCreateRequestDto request, CancellationToken cancellationToken)
    {
        var pc = await _pcService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByIdComponents), new { id = pc.Id }, pc);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(PcResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PcResponseDto>> Update(int id, PcUpdateRequestDto request, CancellationToken cancellationToken)
    {
        var pc = await _pcService.UpdateAsync(id, request, cancellationToken);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _pcService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}

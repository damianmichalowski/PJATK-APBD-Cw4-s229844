using Microsoft.AspNetCore.Mvc;
using RestApi.Api.DTOs;
using RestApi.Api.Services;

namespace RestApi.Api.Controllers;

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
    public async Task<ActionResult<List<PcListItemDto>>> GetAll()
    {
        return Ok(await _pcService.GetAllAsync());
    }

    [HttpGet("{id}/components")]
    public async Task<ActionResult<PcDetailsDto>> GetWithComponents(int id)
    {
        var pc = await _pcService.GetWithComponentsAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PcListItemDto>> Create(CreatePcDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var created = await _pcService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetWithComponents), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PcListItemDto>> Update(int id, UpdatePcDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updated = await _pcService.UpdateAsync(id, dto);

        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}

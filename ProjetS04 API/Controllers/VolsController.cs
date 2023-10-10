using ProjetS04_API.Models;
using ProjetS04_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ProjetS04_API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VolsController : ControllerBase
    {
    private readonly VolsService _volsService;

    public VolsController(VolsService volsService) =>
        _volsService = volsService;

    [HttpGet]
    public async Task<List<Vol>> Get() =>
        await _volsService.GetAsync();
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Vol>> Get(string id)
    {
        var vol = await _volsService.GetAsync(id);

        if (vol is null)
        {
            return NotFound();
        }

        return vol;
    }
    [HttpPost("SearchVol")]
    public async Task<ActionResult<Vol>> GetVol([FromBody] Vol request)
    {
        var vol = await _volsService.GetAsyncByParam(request.villeDepart, request.villeArrivee, request.DateDepart, request.DateArriver, request.heureDepart, request.heureArrivee);

        if (vol is null)
        {
            return NotFound();
        }

        return Ok(vol);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Vol newVol)
    {
        var result = await _volsService.CreateAsync(newVol);
        if (result == null)
        {
            return BadRequest("Vol existe");
        }

        return Ok();
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Vol updatedVol)
    {
        var vol = await _volsService.GetAsync(id);

        if (vol is null)
        {
            return NotFound();
        }

        updatedVol.numeroVol = vol.numeroVol;

        await _volsService.UpdateAsync(id, updatedVol);

        return Ok();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var vol = await _volsService.GetAsync(id);

        if (vol is null)
        {
            return NotFound();
        }

        await _volsService.RemoveAsync(id);

        return Ok();
    }
}


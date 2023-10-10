using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjetS04_API.Models;
using ProjetS04_API.Services;

namespace ProjetS04_API.Controllers;
[ApiController]
[EnableCors("DefaultPolicy")]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationsService _reservationsService;

    public ReservationsController(ReservationsService reservationsService) =>
        _reservationsService = reservationsService;

    [HttpGet]
    public async Task<List<Reservation>> Get() =>
        await _reservationsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Reservation>> Get(string id)
    {
        var reservation = await _reservationsService.GetAsync(id);

        if (reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Reservation newReservation)
    {
        await _reservationsService.CreateAsync(newReservation);

        return CreatedAtAction(nameof(Get), new { id = newReservation.idReservation }, newReservation);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Reservation updatedReservation)
    {
        var reservation = await _reservationsService.GetAsync(id);

        if (reservation is null)
        {
            return NotFound();
        }

        updatedReservation.idReservation = reservation.idReservation;

        await _reservationsService.UpdateAsync(id, updatedReservation);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        
        var reservation = await _reservationsService.GetAsync(id);

        if (reservation is null)
        {
            return NotFound();
        }

        await _reservationsService.RemoveAsync(id);

        return NoContent();
    }
}


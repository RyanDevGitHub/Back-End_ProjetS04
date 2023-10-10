using ProjetS04_API.Models;
using ProjetS04_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace ProjetS04_API.Controllers;
[ApiController]

[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientsService _clientsService;

    public ClientsController(ClientsService clientsService) =>
        _clientsService = clientsService;

    [HttpGet]
    public async Task<List<Client>> Get() =>
        await _clientsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Client>> Get(string id)
    {
        var client = await _clientsService.GetAsync(id);

        if (client is null)
        {
            return NotFound();
        }

        return client;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Client newClient)
    {
        await _clientsService.CreateAsync(newClient);

        return Ok(new { id = newClient.IdClient });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var client = await _clientsService.Login(request.Firstname,request.PassportNumber);
        if (client is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(client.IdClient);
        }
    }



    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Client updatedClient)
    {
        var client = await _clientsService.GetAsync(id);

        if (client is null)
        {
            return NotFound();
        }

        updatedClient.IdClient = client.IdClient;

        await _clientsService.UpdateAsync(id, updatedClient);

        return Ok();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var client = await _clientsService.GetAsync(id);

        if (client is null)
        {
            return NotFound();
        }

        await _clientsService.RemoveAsync(id);

        return Ok();
    }
}

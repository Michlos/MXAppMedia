using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MXAppMedia.Api.DTOs;
using MXAppMedia.Api.Services;

namespace MXAppMedia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{id:int}", Name ="GetClient")]
    public async Task<ActionResult<ClientDTO>> GetClientById(int id)
    {
        var clientDto = await _clientService.GetClientById(id);
        if (clientDto == null)
            return NotFound("Client not found");
        return Ok(clientDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAll()
    {
        var clientsDto = await _clientService.GetAllClients();
        if (clientsDto is null)
            return NotFound("No clients found.");
        return Ok(clientsDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddClient([FromBody] ClientDTO clientDto)
    {
        if (clientDto == null)
            return BadRequest("Client data is null.");
        await _clientService.AddClient(clientDto);
        return CreatedAtRoute("GetClient", new { id = clientDto.Id }, clientDto);
    }

    [HttpPut("{id:int}", Name = "UpdateClient")]
    public async Task<ActionResult> UpdateClient(int id, [FromBody] ClientDTO clientDto)
    {
        if (clientDto == null || clientDto.Id != id)
            return BadRequest("Client data is invalid.");
        
        var existingClient = await _clientService.GetClientById(id);
        if (existingClient == null)
            return NotFound("Client not found.");
        await _clientService.UpdateClient(clientDto);
        //return NoContent();
        return Ok(clientDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteClient")]
    public async Task<ActionResult> DeleteClient(int id)
    {
        var clienteDto = await _clientService.GetClientById(id);
        if (clienteDto == null)
            return NotFound("Client not found.");
        
        await _clientService.DeleteClient(id);
        return Ok(clienteDto);
    }

}

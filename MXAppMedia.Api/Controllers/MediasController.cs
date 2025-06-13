using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MXAppMedia.Api.DTOs;
using MXAppMedia.Api.Services;

namespace MXAppMedia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MediasController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediasController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }

    [HttpGet("{id:int}", Name = "GetMedia")]
    public async Task<ActionResult<MediaDTO>> GetMediaById(int id)
    {
        var mediaDto = await _mediaService.GetMediaById(id);
        if (mediaDto == null)
            return NotFound("Media not found");
        return Ok(mediaDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaDTO>>> GetAll()
    {
        var mediasDto = await _mediaService.GetAllMedias();
        if (mediasDto is null)
            return NotFound("No media found.");
        return Ok(mediasDto);
    }

    [HttpGet("client/{clientId:int}")]
    public async Task<ActionResult<IEnumerable<MediaDTO>>> GetAllByClientId(int clientId)
    {
        var mediasDto = await _mediaService.GetAllMediasByClientId(clientId);
        if (mediasDto is null)
            return NotFound("No media found for this client.");
        return Ok(mediasDto);
    }

    [HttpPost]
    public async Task<ActionResult> AddMedia([FromBody] MediaDTO mediaDto)
    {
        if (mediaDto == null)
            return BadRequest("Media data is null.");
        await _mediaService.AddMedia(mediaDto);
        return CreatedAtRoute("GetMedia", new { id = mediaDto.Id }, mediaDto);
    }

    [HttpPut("{id:int}", Name = "UpdateMedia")]
    public async Task<ActionResult> UpdateMedia(int id, [FromBody] MediaDTO mediaDto)
    {
        if (mediaDto == null || mediaDto.Id != id)
            return BadRequest("Media data is invalid.");
        
        var existingMedia = await _mediaService.GetMediaById(id);
        if (existingMedia == null)
            return NotFound("Media not found.");
        await _mediaService.UpdateMedia(mediaDto);
        //return NoContent();
        return Ok(mediaDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteMedia")]
    public async Task<ActionResult> DeleteMedia(int id)
    {
        var mediaDTO = await _mediaService.GetMediaById(id);
        if (mediaDTO == null)
            return NotFound("Media not found.");
        
        await _mediaService.DeleteMediaAsync(id);
        return Ok(mediaDTO);
    }

}

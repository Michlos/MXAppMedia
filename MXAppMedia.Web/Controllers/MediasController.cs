using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

namespace MXAppMedia.Web.Controllers;

public class MediasController : Controller
{
    private readonly IMediaService _mediaService;
    private readonly IClientService _clientService;

    public MediasController(IMediaService mediaService, IClientService clientService)
    {
        _mediaService = mediaService;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaViewModel>>> Index()
    {
        var result = await _mediaService.GetAllMediaAsync();

        if (result == null || !result.Any())
        {
            return NotFound("No media items found.");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateMedia()
    {
        ViewBag.ClientId = new SelectList(await 
            _clientService.GetAllClientAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedia(MediaViewModel mediaViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _mediaService.AddMediaAsync(mediaViewModel);
            if (result != null)
                return RedirectToAction(nameof(Index));
            //ModelState.AddModelError("", "Failed to create media item.");
        }
        else
        {
            ViewBag.ClientId = new SelectList(await
                _clientService.GetAllClientAsync(), "Id", "Name", mediaViewModel.ClientId);
        }
        return View(mediaViewModel);
    }
}

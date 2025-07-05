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
    public async Task<IActionResult> CreateMedia(int? clientId)
    {
        ViewBag.ClientId = new SelectList(await
            _clientService.GetAllClientAsync(), "Id", "Name", clientId);
        var model = new MediaViewModel();
        if (clientId.HasValue)
        {
            model.ClientId = clientId.Value;
            var client = await _clientService.GetClientByIdAsync(clientId.Value);
            if (client != null)
            {
                model.ClientName = client.Name;
            }
        }
        return View(model);
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

    ////Criar mídia para um cliente específico
    //[HttpPost]
    //public async Task<IActionResult> CreateMedia(int clientId, MediaViewModel mediaViewModel)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        mediaViewModel.ClientId = clientId;
    //        var result = await _mediaService.AddMediaAsync(mediaViewModel);
    //        if (result != null)
    //            return RedirectToAction(nameof(Index));
    //        //ModelState.AddModelError("", "Failed to create media item.");
    //    }
    //    else
    //    {
    //        ViewBag.ClientId = new SelectList(await 
    //            _clientService.GetAllClientAsync(), "Id", "Name", clientId);
    //    }
    //    return View(mediaViewModel);
    //}

    [HttpGet]
    public async Task<IActionResult> CreateMediaClient(int clientId)
    {
        var client = await _clientService.GetClientByIdAsync(clientId);
        var mediaViewModel = new MediaViewModel { ClientId = clientId, ClientName = client.Name };
        return View("CreateMediaClient", mediaViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMediaClient(int clientId, MediaViewModel mediaViewModel)
    {

        //aNÃO TA CRIANDO SOMENTE PEGANDO... TEM QUE CRIAR AQUI
        if (ModelState.IsValid)
        {
            mediaViewModel.ClientId = clientId;
            var result = await _mediaService.AddMediaAsync(mediaViewModel);
            if (result != null)
                return RedirectToAction("ClientDetailView", "Clients", new {id = clientId });
            //ModelState.AddModelError("", "Failed to create media item for client.");
        }
        else
        {
            ViewBag.ClientId = new SelectList(await
                _clientService.GetAllClientAsync(), "Id", "Name", clientId);
            mediaViewModel = new MediaViewModel { ClientId = clientId };
        }
        return View("CreateMediaClient", mediaViewModel);
    }


    [HttpGet]
    public async Task<IActionResult> EditMedia(int id)
    {
        //ViewBag.ClientId = new SelectList(await 
        //    _clientService.GetAllClientAsync(), "Id", "Name");

        var result = await _mediaService.GetMediaByIdAsync(id);

        if (result == null)
            return View("Error");

        return View(result);

    }

    [HttpPost]
    public async Task<IActionResult> EditMedia(MediaViewModel mediaViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _mediaService.UpdateMediaAsync(mediaViewModel);
            if (result != null)
                return RedirectToAction("ClientDetailView", "Clients", new {id = mediaViewModel.ClientId});
            //ModelState.AddModelError("", "Failed to update media item.");
        }
        //else
        //{
        //    ViewBag.ClientId = new SelectList(await 
        //        _clientService.GetAllClientAsync(), "Id", "Name", mediaViewModel.ClientId);
        //}
        return View(mediaViewModel);
    }


    [HttpGet]
    public async Task<ActionResult<MediaViewModel>> DeleteMedia(int id)
    {
        var result = await _mediaService.GetMediaByIdAsync(id);
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost, ActionName("DeleteMedia")]
    public async Task<IActionResult> DeleteMediaConfirmed(int id)
    {
        var result = await _mediaService.DeleteMediaAsync(id);
        if (!result)
            return View("Error");

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MediaViewModel>>> GetMediaByClientId(int clientId)
    {
        var result = await _mediaService.GetAllMediaByClienteIdAsync(clientId);
        if (result == null || !result.Any())
        {
            return NotFound("No media items found for this client.");
        }
        return View("Index", result);
    }
}

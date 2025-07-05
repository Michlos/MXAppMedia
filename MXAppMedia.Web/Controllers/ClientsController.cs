using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

namespace MXAppMedia.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IMediaService _mediaService;
        public ClientsController(IClientService clientService, IMediaService mediaService)
        {
            _clientService = clientService;
            _mediaService = mediaService;
        }

        //CLIENTS LIST 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientViewModel>>> Index()
        {
            var result = await _clientService.GetAllClientAsync();
            if (result == null || !result.Any())
            {
                return NotFound("No clients found.");
            }
            return View(result);
        }

        //CREATE CLIENT GETTING LIST DO CREATE CLIENT
        [HttpGet]
        public async Task<IActionResult> CreateClient()
        {
            ViewBag.ClientId = new SelectList(await _clientService.GetAllClientAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _clientService.AddClientAsync(clientViewModel);
                if (result != null)
                    return RedirectToAction(nameof(Index));
                //ModelState.AddModelError("", "Failed to create client.");
            }
            else
            {
                ViewBag.ClientId = new SelectList(await _clientService.GetAllClientAsync(), "Id", "Name", clientViewModel.Id);
            }
            return View(clientViewModel);
        }

        //DETALHE DO CLIENTE
        [HttpGet]
        public async Task<IActionResult> DetailClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound("Client not found.");
            }

            //trazer mídias do cliente
            ICollection<MediaViewModel> medias =  _mediaService.GetAllMediaByClienteIdAsync(id).Result.ToList();

            return View(client);
        }

        [HttpGet]
        public async Task<ActionResult<ClientViewModel>> ClientDetailView(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound("Client not found.");
            }
            //trazer mídias do cliente
            //ICollection<MediaViewModel> medias = (ICollection<MediaViewModel>)await _mediaService.GetAllMediaByClienteIdAsync(id);
            //client.Medias = medias.ToList();
            client.Medias = await _mediaService.GetAllMediaByClienteIdAsync(id);
            return View(client);
        }

    }
}

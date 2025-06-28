using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

namespace MXAppMedia.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }
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
    }
}

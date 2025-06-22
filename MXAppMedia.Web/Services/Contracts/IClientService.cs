using MXAppMedia.Web.Models;

namespace MXAppMedia.Web.Services.Contracts;

public interface IClientService
{
    Task<IEnumerable<ClientViewModel>> GetAllClientAsync();
    Task<ClientViewModel> GetClientByIdAsync(int id);
    Task<ClientViewModel> AddClientAsync(ClientViewModel clientViewModel);
    Task<ClientViewModel> UpdateClientAsync(ClientViewModel clientViewModel);
    Task<bool> DeleteClientAsync(int id);
}

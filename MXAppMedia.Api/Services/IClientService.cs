using MXAppMedia.Api.DTOs;

namespace MXAppMedia.Api.Services;

public interface IClientService
{

    Task<ClientDTO> GetClientById(int id);
    Task<IEnumerable<ClientDTO>> GetAllClients();
    Task AddClient(ClientDTO clientDto);
    Task UpdateClient(ClientDTO clientDto);
    Task DeleteClient(int id);

}

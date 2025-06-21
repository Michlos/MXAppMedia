using AutoMapper;

using MXAppMedia.Api.DTOs;
using MXAppMedia.Api.Models;
using MXAppMedia.Api.Repositories;

namespace MXAppMedia.Api.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        _clientRepository = clientRepository;
        _mapper = mapper;
    }

    public async Task AddClient(ClientDTO clientDto)
    {
        var clientEntity = _mapper.Map<Client>(clientDto);
        await _clientRepository.AddClientAsync(clientEntity);
        clientDto.Id = clientEntity.Id;

    }

    public async Task DeleteClient(int id)
    {
        var clientEntity = _clientRepository.GetClientByIdAsync(id);
        await _clientRepository.DeleteClientAsync(clientEntity.Id);
    }

    public async Task<IEnumerable<ClientDTO>> GetAllClients()
    {
        var clientEntity = await _clientRepository.GetAllClientsAsync();
        return _mapper.Map<IEnumerable<ClientDTO>>(clientEntity);
    }

    public async Task<ClientDTO> GetClientById(int id)
    {
        var clientEntity = await _clientRepository.GetClientByIdAsync(id);
        return _mapper.Map<ClientDTO>(clientEntity);

    }

    public async Task UpdateClient(ClientDTO clientDto)
    {
        var clientEntity = _mapper.Map<Client>(clientDto);
        await _clientRepository.UpdateClientAsync(clientEntity);
    }
}

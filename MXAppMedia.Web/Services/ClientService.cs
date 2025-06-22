using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

using System.Text.Json;

namespace MXAppMedia.Web.Services;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "api/clients";
    private readonly JsonSerializerOptions _options;
    private ClientViewModel clienteViewModel;
    private IEnumerable<ClientViewModel> clientsViewModel;

    public ClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ClientViewModel>> GetAllClientAsync()
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        IEnumerable<ClientViewModel> listClientViewModel;

        var response = await client.GetAsync(apiEndPoint);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            listClientViewModel = await JsonSerializer
                .DeserializeAsync<IEnumerable<ClientViewModel>>(apiResponse, _options);
        }
        else
        {
            return null;
        }
        return listClientViewModel;

    }
    public Task<ClientViewModel> GetClientByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ClientViewModel> AddClientAsync(ClientViewModel clientViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<ClientViewModel> UpdateClientAsync(ClientViewModel clientViewModel)
    {
        throw new NotImplementedException();
    }
    public Task<bool> DeleteClientAsync(int id)
    {
        throw new NotImplementedException();
    }
}

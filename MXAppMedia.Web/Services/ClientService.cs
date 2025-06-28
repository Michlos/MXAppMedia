using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

using System.Diagnostics.Eventing.Reader;
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
    public async Task<ClientViewModel> GetClientByIdAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                clienteViewModel = await JsonSerializer
                    .DeserializeAsync<ClientViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
            return clienteViewModel;
        }

    }

    public async Task<ClientViewModel> AddClientAsync(ClientViewModel clientViewModel)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        StringContent content = new StringContent(JsonSerializer.Serialize(clientViewModel),
            System.Text.Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                clienteViewModel = await JsonSerializer
                    .DeserializeAsync<ClientViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return clienteViewModel;
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

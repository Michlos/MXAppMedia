using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace MXAppMedia.Web.Services;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "api/Clients/";
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
        IEnumerable<ClientViewModel>? listClientViewModel = null; // Use nullable type to avoid CS8600

        var response = await client.GetAsync(apiEndPoint);
        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            listClientViewModel = await JsonSerializer
                .DeserializeAsync<IEnumerable<ClientViewModel>>(apiResponse, _options);
        }
        else
        {
            return Enumerable.Empty<ClientViewModel>(); // Return an empty collection instead of null
        }
        return listClientViewModel ?? Enumerable.Empty<ClientViewModel>(); // Ensure a non-null return value
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
                    .DeserializeAsync<ClientViewModel>(apiResponse, _options)
                    ?? new ClientViewModel(); // Ensure clienteViewModel is not null
            }
        }
        return clienteViewModel ?? new ClientViewModel(); // Return a new instance if null
    }

    public async Task<ClientViewModel?> AddClientAsync(ClientViewModel clientViewModel) // Use nullable return type
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        StringContent content = new StringContent(JsonSerializer.Serialize(clientViewModel),
            System.Text.Encoding.UTF8, "application/json");

        ClientViewModel? resultClientViewModel = null; // Initialize with null to avoid CS8601

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                resultClientViewModel = await JsonSerializer
                    .DeserializeAsync<ClientViewModel>(apiResponse, _options);
            }
        }
        return resultClientViewModel; // Return null if not successful
    }

    public async Task<ClientViewModel?> UpdateClientAsync(ClientViewModel clientViewModel) // Use nullable return type
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        StringContent content = new StringContent(JsonSerializer.Serialize(clientViewModel),
            System.Text.Encoding.UTF8, "application/json");

        ClientViewModel? updatedClientViewModel = null; // Initialize with null to avoid CS8601

        using (var response = await client.PutAsync(apiEndPoint + clientViewModel.Id, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                updatedClientViewModel = await JsonSerializer
                    .DeserializeAsync<ClientViewModel>(apiResponse, _options);
            }
        }
        return updatedClientViewModel; // Return null if not successful
    }
    public async Task<bool> DeleteClientAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");
        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true; // Return true if deletion was successful
            }
            else
            {
                return false; // Return false if deletion failed
            }
        }

    }
}

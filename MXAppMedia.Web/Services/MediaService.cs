using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

using System.Text;
using System.Text.Json;

namespace MXAppMedia.Web.Services;

public class MediaService : IMediaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "/api/Medias/";
    private readonly JsonSerializerOptions _options;
    private MediaViewModel mediaViewModel;
    private IEnumerable<MediaViewModel> listMediaViewModel;

    public MediaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<MediaViewModel>> GetAllMediaAsync()
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        using (var response = await client.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                listMediaViewModel = await JsonSerializer
                    .DeserializeAsync<IEnumerable<MediaViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return listMediaViewModel;
    }

    public async Task<MediaViewModel> GetMediaByIdAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                mediaViewModel = await JsonSerializer
                    .DeserializeAsync<MediaViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }

        }
        return mediaViewModel;

    }
    public async Task<IEnumerable<MediaViewModel>> GetAllMediaByClienteIdAsync(int clienteId)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        using (var response = await client.GetAsync(apiEndPoint + clienteId))
        {
            if (response.IsSuccessStatusCode)
            {
                var apíResponse = await response.Content.ReadAsStreamAsync();
                listMediaViewModel = await JsonSerializer
                    .DeserializeAsync<IEnumerable<MediaViewModel>>(apíResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return listMediaViewModel;
    }

    public async Task<MediaViewModel> AddMediaAsync(MediaViewModel mediaViewModel)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(mediaViewModel),
            Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                mediaViewModel = await JsonSerializer
                    .DeserializeAsync<MediaViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return mediaViewModel;
    }

    public async Task<MediaViewModel> UpdateMediaAsync(MediaViewModel mediaViewModel)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        MediaViewModel mediaUpdated = new MediaViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndPoint, mediaViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                mediaUpdated = await JsonSerializer
                    .DeserializeAsync<MediaViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return mediaUpdated;
    }
    public async Task<bool> DeleteMediaAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("MediaApi");

        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

}

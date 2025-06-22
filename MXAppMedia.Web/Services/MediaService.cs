using MXAppMedia.Web.Models;
using MXAppMedia.Web.Services.Contracts;

using System.Text.Json;

namespace MXAppMedia.Web.Services;

public class MediaService : IMediaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndpoint = "/api/medias";
    private readonly JsonSerializerOptions _options;
    private MediaViewModel mediaViewModel;
    private IEnumerable<MediaViewModel> mediasViewModel;

    public MediaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public Task<IEnumerable<MediaViewModel>> GetAllMediaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<MediaViewModel> GetMediaByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IEnumerable<MediaViewModel>> GetAllMediaByClienteIdAsync(int clienteId)
    {
        throw new NotImplementedException();
    }

    public Task<MediaViewModel> AddMediaAsync(MediaViewModel mediaViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<MediaViewModel> UpdateMediaAsync(MediaViewModel mediaViewModel)
    {
        throw new NotImplementedException();
    }
    public Task<bool> DeleteMediaAsync(int id)
    {
        throw new NotImplementedException();
    }

}

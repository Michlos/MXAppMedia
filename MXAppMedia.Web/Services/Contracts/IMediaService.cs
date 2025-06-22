using MXAppMedia.Web.Models;

namespace MXAppMedia.Web.Services.Contracts;

public interface IMediaService
{
    Task<IEnumerable<MediaViewModel>> GetAllMediaAsync();
    Task<MediaViewModel> GetMediaByIdAsync(int id);
    Task<IEnumerable<MediaViewModel>> GetAllMediaByClienteIdAsync(int clienteId);
    Task<MediaViewModel> AddMediaAsync(MediaViewModel mediaViewModel);
    Task<MediaViewModel> UpdateMediaAsync(MediaViewModel mediaViewModel);
    Task<bool> DeleteMediaAsync(int id);
}

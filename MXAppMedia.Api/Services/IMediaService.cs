using MXAppMedia.Api.DTOs;
using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Services;

public interface IMediaService
{
    Task<MediaDTO> GetMediaById(int id);
    Task<IEnumerable<MediaDTO>> GetAllMedias();
    Task<IEnumerable<MediaDTO>> GetAllMediasByClientId(int clientId);
    Task AddMedia(MediaDTO mediaDto);
    Task UpdateMedia(MediaDTO mediaDto);
    Task DeleteMediaAsync(int id);

}

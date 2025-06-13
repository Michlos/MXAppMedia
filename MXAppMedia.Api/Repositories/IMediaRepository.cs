using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Repositories;

public interface IMediaRepository
{

    Task<Media?> GetMediaByIdAsync(int id);
    Task<IEnumerable<Media>> GetAllMediaAsync();
    Task<IEnumerable<Media>> GetAllMediaByClientId(int clientId);
    Task<Media> AddMediaAsync(Media media);
    Task<Media> UpdateMediaAsync(Media media);
    Task<Media?> DeleteMediaAsync(int id);


}

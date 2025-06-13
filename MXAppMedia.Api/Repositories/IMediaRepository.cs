using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Repositories;

public interface IMediaRepository
{
    /// <summary>
    /// Gets the media by its ID.
    /// </summary>
    /// <param name="id">The ID of the media.</param>
    /// <returns>The media if found; otherwise, null.</returns>
    Task<Media?> GetMediaByIdAsync(int id);
    /// <summary>
    /// Gets all media.
    /// </summary>
    /// <returns>A list of all media.</returns>
    Task<IEnumerable<Media>> GetAllMediaAsync();
    /// <summary>
    /// Adds a new medi.
    /// </summary>
    /// <param name="media">The media to add.</param>
    /// <returns>The added media.</returns>
    Task<Media> AddMediaAsync(Media media);
    /// <summary>
    /// Updates an existing media.
    /// </summary>
    /// <param name="media">The media to update.</param>
    /// <returns>The updated media.</returns>
    Task<Media> UpdateMediaAsync(Media media);
    /// <summary>
    /// Deletes a media by its ID.
    /// </summary>
    /// <param name="id">The ID of the media to delete.</param>
    Task<Media?> DeleteMediaAsync(int id);


}

using AutoMapper;

using MXAppMedia.Api.DTOs;
using MXAppMedia.Api.Models;
using MXAppMedia.Api.Repositories;

namespace MXAppMedia.Api.Services;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;
    private readonly IMapper _mapper;

    public MediaService(IMediaRepository mediaRepository, IMapper mapper)
    {
        _mediaRepository = mediaRepository;
        _mapper = mapper;
    }

    public async Task AddMedia(MediaDTO mediaDto)
    {
        var mediaEntity = _mapper.Map<Media>(mediaDto);
        await _mediaRepository.AddMediaAsync(mediaEntity);
    }

    public async Task DeleteMediaAsync(int id)
    {
        var mediaEntity = _mediaRepository.GetMediaByIdAsync(id);
        await _mediaRepository.DeleteMediaAsync(mediaEntity.Id);
    }

    public async Task<IEnumerable<MediaDTO>> GetAllMedias()
    {
        var mediaEntities = await _mediaRepository.GetAllMediaAsync();
        return _mapper.Map<IEnumerable<MediaDTO>>(mediaEntities);
    }

    public async Task<IEnumerable<MediaDTO>> GetAllMediasByClientId(int clientId)
    {
        var mediaEntities = await _mediaRepository.GetAllMediaByClientId(clientId);
        return _mapper.Map<IEnumerable<MediaDTO>>(mediaEntities);
    }

    public Task<MediaDTO> GetMediaById(int id)
    {
        var mediaEntity = _mediaRepository.GetMediaByIdAsync(id);
        return _mapper.Map<Task<MediaDTO>>(mediaEntity);
    }

    public async Task UpdateMedia(MediaDTO mediaDto)
    {
        var mediaEntity = _mapper.Map<Media>(mediaDto);
        await _mediaRepository.UpdateMediaAsync(mediaEntity);
    }
}

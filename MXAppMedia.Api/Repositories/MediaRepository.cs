using Microsoft.EntityFrameworkCore;

using MXAppMedia.Api.Context;
using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Media> AddMediaAsync(Media media)
    {
        
        await _context.Medias.AddAsync(media);
        await _context.SaveChangesAsync();
        return media;
        
    }

    public async Task<Media?> DeleteMediaAsync(int id)
    {
        var media = await _context.Medias.FindAsync(id);
        if (media == null) return null;
        _context.Medias.Remove(media);
        await _context.SaveChangesAsync();
        return media;

    }

    public async Task<IEnumerable<Media>> GetAllMediaAsync()
    {
        return await _context.Medias
            .ToListAsync();
    }

    public async Task<IEnumerable<Media>> GetAllMediaByClientId(int clientId)
    {
        return await _context.Medias
           .Include(m => m.Client) // Include related Client data if needed
           .Where(m => m.ClientId == clientId)
           .ToListAsync();
    }

    public async Task<Media?> GetMediaByIdAsync(int id)
    {
        return await _context.Medias
            .Include(m => m.Client) // Include related Client data if needed
            .Where(m => m.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Media> UpdateMediaAsync(Media media)
    {
        var existingMedia = await _context.Medias.AsNoTracking().FirstOrDefaultAsync(m => m.Id == media.Id);
        if (existingMedia == null)
            throw new InvalidOperationException("Media not found.");

        var entry = _context.Attach(media);

        if(media.Title is not null && media.Title != existingMedia.Title)
            entry.Property(m => m.Title).IsModified = true;
        if (media.Description is not null && media.Description != existingMedia.Description)
            entry.Property(m => m.Description).IsModified = true;
        if (media.MediaUrl is not null && media.MediaUrl != existingMedia.MediaUrl)
            entry.Property(m => m.MediaUrl).IsModified = true;
        if(media.IsActive != existingMedia.IsActive)
            entry.Property(m => m.IsActive).IsModified = true;

        await _context.SaveChangesAsync();
        return media;

    }
}

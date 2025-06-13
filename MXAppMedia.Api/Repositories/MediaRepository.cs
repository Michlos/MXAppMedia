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
            .Include(m => m.Client) // Include related Client data if needed
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
        _context.Entry(media).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return media;

    }
}

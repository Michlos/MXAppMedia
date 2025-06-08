using Microsoft.EntityFrameworkCore;

using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Media> Medias { get; set; }
    public DbSet<Client> Clients { get; set; }

    //FLUENT API


}

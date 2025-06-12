using Microsoft.EntityFrameworkCore;

using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Media> Medias { get; set; }
    public DbSet<Client> Clients { get; set; }

    //FLUENT API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Client
        mb.Entity<Client>().HasKey(c => c.Id);
        mb.Entity<Client>().Property(c => c.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Client>().Property(c => c.CNPJ).HasMaxLength(50);
        mb.Entity<Client>().Property(c => c.CPF).HasMaxLength(50);
        mb.Entity<Client>().Property(c => c.ContactPerson).HasMaxLength(100);
        mb.Entity<Client>().Property(c => c.Email).HasMaxLength(100);
        mb.Entity<Client>().HasIndex(c => c.Email).IsUnique();
        mb.Entity<Client>().HasIndex(c => c.CNPJ).IsUnique();
        mb.Entity<Client>().HasIndex(c => c.CPF).IsUnique();
        mb.Entity<Client>().HasMany(c => c.Medias).WithOne(m => m.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);

        //Media
        mb.Entity<Media>().HasKey(m => m.Id);
        mb.Entity<Media>().Property(m => m.Title).IsRequired().HasMaxLength(100);
        mb.Entity<Media>().Property(m => m.Description).HasMaxLength(100);
        mb.Entity<Media>().Property(m => m.MediaUrl).IsRequired().HasMaxLength(255);
    }
}


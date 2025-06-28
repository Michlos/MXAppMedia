using Microsoft.EntityFrameworkCore;

using MXAppMedia.Api.Context;
using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client> AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client?> DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id); 
        if (client == null) return null;
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<IEnumerable<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        return await _context.Clients
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Client> UpdateClientAsync(Client client)
    {

        //_context.Entry(client).State = EntityState.Modified;
        //await _context.SaveChangesAsync();
        //return client;
        var existingClient = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == client.Id);
        if (existingClient == null)
            throw new InvalidOperationException("Cliente não encontrado.");

        var entry = _context.Attach(client);

        // Só marca como modificado se o valor mudou
        if (client.Name is not null && client.Name != existingClient.Name)
            entry.Property(c => c.Name).IsModified = true;
        if (client.CNPJ is not null && client.CNPJ != existingClient.CNPJ)
            entry.Property(c => c.CNPJ).IsModified = true;
        if (client.CPF is not null && client.CPF != existingClient.CPF)
            entry.Property(c => c.CPF).IsModified = true;
        if (client.ContactPerson is not null && client.ContactPerson != existingClient.ContactPerson)
            entry.Property(c => c.ContactPerson).IsModified = true;
        if (client.Email is not null && client.Email != existingClient.Email)
            entry.Property(c => c.Email).IsModified = true;
        if (client.IsActive != existingClient.IsActive)
            entry.Property(c => c.IsActive).IsModified = true;

        await _context.SaveChangesAsync();
        return existingClient;
    }
}

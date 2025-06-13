using MXAppMedia.Api.Models;

namespace MXAppMedia.Api.Repositories;

public interface IClientRepository
{
    /// <summary>
    /// Retrieves a client by its ID.
    /// </summary>
    /// <param name="id">The ID of the client.</param>
    /// <returns>The client with the specified ID, or null if not found.</returns>
    Task<Client?> GetClientByIdAsync(int id);
    /// <summary>
    /// Retrieves all clients.
    /// </summary>
    /// <returns>A list of all clients.</returns>
    Task<IEnumerable<Client>> GetAllClientsAsync();
    /// <summary>
    /// Adds a new client.
    /// </summary>
    /// <param name="client">The client to add.</param>
    /// <returns>The added client.</returns>
    Task<Client> AddClientAsync(Client client);
    /// <summary>
    /// Updates an existing client.
    /// </summary>
    /// <param name="client">The client to update.</param>
    /// <returns>The updated client.</returns>
    Task<Client> UpdateClientAsync(Client client);
    /// <summary>
    /// Deletes a client by its ID.
    /// </summary>
    /// <param name="id">The ID of the client to delete.</param>
    Task<Client?> DeleteClientAsync(int id);
}

namespace MXAppMedia.Api.Models;

public class Media
{
    public int Id { get; set; }
    public string? Title { get; set; }       
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public bool IsActive { get; set; } = false;

}

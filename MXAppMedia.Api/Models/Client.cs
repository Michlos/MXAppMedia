namespace MXAppMedia.Api.Models;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CNPJ { get; set; }
    public string? CPF { get; set; }

    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public ICollection<Media>? Medias { get; set; }
    public bool IsActive { get; set; } = true;
}
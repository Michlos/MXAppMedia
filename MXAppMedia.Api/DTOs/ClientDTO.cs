using MXAppMedia.Api.Models;

using System.ComponentModel.DataAnnotations;

namespace MXAppMedia.Api.DTOs;

public class ClientDTO
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage ="Client Name is required")]
    [MinLength(3, ErrorMessage ="Client Name must be minimal 3 characters")]
    [MaxLength(100, ErrorMessage ="Client Name must be maximal 5 characters")]
    public string? Name { get; set; }
    public string? CNPJ { get; set; }
    public string? CPF { get; set; }
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
}

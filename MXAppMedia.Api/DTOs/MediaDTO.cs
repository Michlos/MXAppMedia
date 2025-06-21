using MXAppMedia.Api.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MXAppMedia.Api.DTOs;

public class MediaDTO
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage ="Media Title is required")]
    [StringLength(100)]
    [MinLength(5, ErrorMessage ="Media Title must be minimal 5 characters")]
    [MaxLength(100, ErrorMessage ="Media Title must be maximal 100 characters")]
    public string? Title { get; set; }
    
    [StringLength(100)]
    [MaxLength(100, ErrorMessage ="Description must be maximal 100 characters")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "The URL Media is required")]
    [StringLength(255)]
    [MaxLength(255, ErrorMessage ="The URL Media must be maximal 255 characters")]
    public string? MediaUrl { get; set; }

    public string? ClientName { get; set; }

    [Required(ErrorMessage = "Yout must informe the valid Id from client")]
    [DeniedValues(0, ErrorMessage = "You must be informe the valid Cliente Id")]
    public int ClientId { get; set; }

    [JsonIgnore]
    public Client? Client { get; set; }
    public bool IsActive { get; set; }

}

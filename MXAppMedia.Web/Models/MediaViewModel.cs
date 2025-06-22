using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MXAppMedia.Web.Models;

public class MediaViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? MediaUrl { get; set; }
    
    [Display(Name = "Clients")]
    public string? ClientName { get; set; }

    public int ClientId { get; set; }
    public bool IsActive { get; set; }
}

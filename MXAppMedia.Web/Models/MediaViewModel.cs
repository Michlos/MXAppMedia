using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MXAppMedia.Web.Models;

public class MediaViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }
    public int ClientId { get; set; }
    public string? ClientName { get; set; }
    public bool IsActive { get; set; }
}

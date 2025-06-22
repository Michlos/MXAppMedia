using MXAppMedia.Api.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MXAppMedia.Api.DTOs;

public class MediaDTO
{
    public int Id { get; set; }
    
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? MediaUrl { get; set; }

    public string? ClientName { get; set; }

    //[Required(ErrorMessage = "Yout must informe the valid Id from client")]
    //[DeniedValues(0, ErrorMessage = "You must be informe the valid Cliente Id")]
    public int ClientId { get; set; }

    [JsonIgnore]
    public Client? Client { get; set; }
    public bool IsActive { get; set; }

}

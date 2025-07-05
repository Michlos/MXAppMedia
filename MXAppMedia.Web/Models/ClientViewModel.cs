using System.ComponentModel.DataAnnotations;

namespace MXAppMedia.Web.Models;

public class ClientViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CNPJ { get; set; }
    public string? CPF { get; set; }

    [Display(Name = "Contact")]
    public string? ContactPerson { get; set; }
    public string? Email { get; set; }
    public virtual IEnumerable<MediaViewModel>? Medias { get; set; }
}

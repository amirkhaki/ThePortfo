using System.ComponentModel.DataAnnotations;

namespace ThePortfo.Models.DTOs;
public class ProfileDTO
{

	[Required] public string? Name { get; set; }
	[Required] public string? Title { get; set; }
	[Required] public string? Location { get; set; }
	[Phone] public string? PhoneNumber { get; set; }
	public string AboutMe { get; set; } = string.Empty;
	public int TemplateId { get; set; }
}

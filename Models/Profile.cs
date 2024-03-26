using System.ComponentModel.DataAnnotations;

namespace ThePortfo.Models;
public class Profile
{
	public int Id { get; set; }

	[Required] public string? Name { get; set; }
	[Required] public string? Title { get; set; }
	[Required] public string? Location { get; set; }
	[Phone] public string? PhoneNumber { get; set; }
	public string AboutMe { get; set; } = string.Empty;
	[Url] public string ProfilePhoto {get; set; } = "https://placehold.co/400";
	public required string UserId { get; set; }
	public ApplicationUser? User { get; set; }

	public int TemplateId { get; set; }
	public Template? Template { get; set; }
	public ICollection<PortfolioItem> Items { get; set; } = new List<PortfolioItem>();
	public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}

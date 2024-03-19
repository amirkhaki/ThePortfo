using System.ComponentModel.DataAnnotations;

namespace ThePortfo.Models;


public class PortfolioItem
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	[Url]
	public string ImageUrl { get; set; } = string.Empty;
	public required Profile Profile { get; set; }
}

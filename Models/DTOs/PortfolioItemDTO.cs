using System.ComponentModel.DataAnnotations;

namespace ThePortfo.Models.DTOs;

public class PortfolioItemDTO
{
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	[Url]
	public string ImageUrl { get; set; } = string.Empty;
}

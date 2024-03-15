using System.ComponentModel.DataAnnotations;

namespace ThePortfo.Models;
public class Profile
{
	public int Id { get; set; }

	[Required] public string? Name { get; set; }
	[Required] public string? Title { get; set; }
	[Required] public string? Location { get; set; }
	[Phone] public string? PhoneNumber { get; set; }
	public required string UserId {get; set;}
	public ApplicationUser? User { get; set; }
}

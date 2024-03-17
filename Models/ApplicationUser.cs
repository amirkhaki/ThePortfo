using Microsoft.AspNetCore.Identity;

namespace ThePortfo.Models;

public class ApplicationUser : IdentityUser
{
	public Profile? Profile { get; set; }
	public ICollection<Template> Templates { get; } = new List<Template>();
}

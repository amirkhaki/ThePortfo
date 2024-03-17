namespace ThePortfo.Models;

public class Template
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string LayoutHTML { get; set; } = string.Empty;
	public required ApplicationUser User { get; set; }
}

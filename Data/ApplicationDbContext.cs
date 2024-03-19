using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThePortfo.Models;

namespace ThePortfo.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ThePortfo.Models.Profile> Profile { get; set; } = default!;

public DbSet<ThePortfo.Models.Template> Template { get; set; } = default!;

public DbSet<ThePortfo.Models.PortfolioItem> PortfolioItem { get; set; } = default!;

public DbSet<ThePortfo.Models.Skill> Skill { get; set; } = default!;
}

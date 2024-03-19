using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThePortfo.Data;
using ThePortfo.Models;
using ThePortfo.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ThePortfo.Controllers;

[Authorize]
public class PortfoliosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public PortfoliosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    // GET: Portfolios
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        return View(
            await _context.PortfolioItem
            .Where(p => p.Profile == user!.Profile)
            .ToListAsync()
        );
    }

    // GET: Portfolios/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();

        var portfolioItem = await _context.PortfolioItem
            .Where(p => p.Profile == user!.Profile)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (portfolioItem == null)
        {
            return NotFound();
        }

        return View(portfolioItem);
    }

    // GET: Portfolios/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Portfolios/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PortfolioItemDTO portfolioItem)
    {
        if (!ModelState.IsValid)
        {
            return View(portfolioItem);
        }
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        if (user!.Profile == null)
        {
            return RedirectToAction("Index", "Profile", new { area = "" });
        }
        var portItem = _mapper.Map<PortfolioItem>(portfolioItem);
        portItem.Profile = user.Profile;
        _context.Add(portItem);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Portfolios/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        var portfolioItem = await _context.PortfolioItem
            .Where(p => p.Profile == user!.Profile)
            .SingleOrDefaultAsync(m => m.Id == id);
        if (portfolioItem == null)
        {
            return NotFound();
        }
        return View(portfolioItem);
    }

    // POST: Portfolios/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl")] PortfolioItem portfolioItem)
    {
        if (id != portfolioItem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(portfolioItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioItemExists(portfolioItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(portfolioItem);
    }

    // GET: Portfolios/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();

        var portfolioItem = await _context.PortfolioItem
            .Where(p => p.Profile == user!.Profile)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (portfolioItem == null)
        {
            return NotFound();
        }

        return View(portfolioItem);
    }

    // POST: Portfolios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();

        var portfolioItem = await _context.PortfolioItem
            .Where(p => p.Profile == user!.Profile)
            .SingleOrDefaultAsync(m => m.Id == id);

        if (portfolioItem != null)
        {
            _context.PortfolioItem.Remove(portfolioItem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PortfolioItemExists(int id)
    {
        return _context.PortfolioItem.Any(e => e.Id == id);
    }
}


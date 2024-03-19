using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThePortfo.Data;
using ThePortfo.Models;
using ThePortfo.Models.DTOs;

namespace ThePortfo.Controllers;

[Authorize]
public class SkillsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public SkillsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    // GET: Skills
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        return View(
            await _context.Skill
            .Where(s => s.Profile == user!.Profile)
            .ToListAsync()
        );
    }

    // GET: Skills/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        var skill = await _context.Skill
            .Where(s => s.Profile == user!.Profile)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (skill == null)
        {
            return NotFound();
        }

        return View(skill);
    }

    // GET: Skills/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Skills/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SkillDTO skill)
    {
        if (!ModelState.IsValid)
        {
            return View(skill);
        }
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        if (user!.Profile == null)
        {
            return RedirectToAction("Index", "Profile", new { area = "" });
        }
        var _skill = _mapper.Map<Skill>(skill);
        _skill.Profile = user.Profile;
        _context.Add(_skill);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Skills/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        var skill = await _context.Skill
            .Where(s => s.Profile == user!.Profile)
            .SingleOrDefaultAsync(s => s.Id == id);
        if (skill == null)
        {
            return NotFound();
        }
        return View(_mapper.Map<SkillDTO>(skill));
    }

    // POST: Skills/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SkillDTO skill)
    {

        if (!ModelState.IsValid)
        {
            return View(skill);
        }
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        if (user!.Profile == null)
        {
            return RedirectToAction("Index", "Profile", new { area = "" });
        }
        var _skill = _mapper.Map<Skill>(skill);
        _skill.Id = id;
        _skill.Profile = user.Profile;
        try
        {
            _context.Update(_skill);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SkillExists(id))
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

    // GET: Skills/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        var skill = await _context.Skill
            .Where(s => s.Profile == user!.Profile)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (skill == null)
        {
            return NotFound();
        }

        return View(skill);
    }

    // POST: Skills/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();
        var skill = await _context.Skill
            .Where(s => s.Profile == user!.Profile)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (skill != null)
        {
            _context.Skill.Remove(skill);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SkillExists(int id)
    {
        return _context.Skill.Any(e => e.Id == id);
    }
}

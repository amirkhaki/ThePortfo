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
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public ProfileController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    // GET: Profile
    public async Task<IActionResult> Index()
    {
        var profile = await _context.Profile.SingleOrDefaultAsync(x => x.UserId == _userManager.GetUserId(User));
        return View(_mapper.Map<ProfileDTO>(profile));
    }

    // POST: Profile
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProfileDTO profile)
    {
        if (!ModelState.IsValid)
        {
            return View(profile);
        }

        var saveProfile = _mapper.Map<ThePortfo.Models.Profile>(profile);

        var user = await _userManager.GetUserAsync(User);
        await _context.Entry(user!).Reference(u => u.Profile).LoadAsync();

        if (user!.Profile == null)
        {
            saveProfile.UserId = user.Id;
            _context.Profile.Add(saveProfile);
            await _context.SaveChangesAsync();
            TempData["updated"] = true;
            return RedirectToAction(nameof(Index));
        }

        saveProfile.UserId = user.Id;
        saveProfile.Id = user.Profile.Id;
        user.Profile = saveProfile;
        await _context.SaveChangesAsync();
        TempData["updated"] = true;
        return RedirectToAction(nameof(Index));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThePortfo.Data;
using ThePortfo.Models;
using ThePortfo.Models.DTOs;

namespace ThePortfo.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TemplatesController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Template.Include(t => t.User).ToListAsync());
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Template
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TemplateDTO template)
        {
            if (ModelState.IsValid)
            {
                var templ = _mapper.Map<Template>(template);
                var user = await _userManager.GetUserAsync(User);
                templ.User = user!;
                _context.Add(templ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var user = await _userManager.GetUserAsync(User);
            var template = await _context.Template.Include(t => t.User)
                .SingleOrDefaultAsync(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            if (template.User != user) {
                return Forbid();
            }
            return View(_mapper.Map<TemplateDTO>(template));
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TemplateDTO template)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var templ = _mapper.Map<Template>(template);
                    templ.Id = id;
                    _context.Update(templ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(id))
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
            return View(template);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var template = await _context.Template
                .FirstOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Template.FindAsync(id);
            if (template != null)
            {
                _context.Template.Remove(template);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Template.Any(e => e.Id == id);
        }
    }
}

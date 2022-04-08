using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VarArbitro.Models;
using VarArbitro.Models.ViewModels;

namespace VarArbitro.Controllers
{
    public class BeerController : Controller
    {

        private readonly PubContext _context;

        public BeerController(PubContext context)
        {
            _context = context;
        }
        
        public async Task<ViewResult> Index()
        {
            var beers = _context.Beers.Include(b => b.Brand);
            return View(await beers.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var beer = new Beer()
                {
                    Name = model.Name,
                    BrandId = model.BrandId
                };
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "BrandId", "Name");
            return View(model);
        }
    }
}
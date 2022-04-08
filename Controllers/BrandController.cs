using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VarArbitro.Models;

namespace VarArbitro.Controllers;


public class BrandController : Controller
{
    //readonly solo permite asisgnacion de datos en constructor
    private readonly PubContext _context;

    public BrandController(PubContext context)
    {
        _context = context;
    }
    
    // GET
    public async Task<IActionResult> Index()
    => View(await _context.Brands.ToListAsync());
    
    
    //CREATE
}
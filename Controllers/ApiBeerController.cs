using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VarArbitro.Models;
using VarArbitro.Models.ViewModels;

namespace VarArbitro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBeerController : ControllerBase
    {
        private readonly PubContext _context;

        public ApiBeerController(PubContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Task<Beer>>> Get()
        {
            var data = (from beer in _context.Beers
                join brand in _context.Brands on beer.BrandId equals brand.BrandId
                select new
                {
                    Cerveza = beer.Name, Marca = brand.Name
                }).ToListAsync();
            
            return Ok(await data);
        }
    } 
}
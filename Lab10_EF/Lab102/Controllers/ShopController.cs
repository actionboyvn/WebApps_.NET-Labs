using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab102.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab102.Controllers
{
    public class ShopController : Controller
    {
        private readonly Context _context;
        public ShopController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }       
        
        [HttpPost]
        public async Task<IActionResult> Details(int? Id)
        {
            var context = _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == Id);                  
            return View(await context.ToListAsync());
        }
    }
}

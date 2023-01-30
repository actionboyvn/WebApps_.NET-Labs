using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;
using Lab12.Models;

namespace Lab12.Pages.Shop
{
    public class DetailsModel : PageModel
    {
        private readonly Lab12.Data.MyDbContext _context;

        public DetailsModel(Lab12.Data.MyDbContext context)
        {
            _context = context;
        }
        public IList<Article> Article { get;set; }
        [BindProperty]
        public int? Id { get; set; }
        public async Task OnPostAsync()
        {
            
            if (Id == null)
            {
                return;
            }           
            Article = await _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == Id).ToListAsync();
           
            ViewData["Category"] = "Category: " + _context.Categories.First(c => c.Id == Id).Name;
         
        }
    }
}

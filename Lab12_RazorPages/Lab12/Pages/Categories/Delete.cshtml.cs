using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;
using Lab12.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lab12.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly Lab12.Data.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public DeleteModel(Lab12.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Categories.FindAsync(id);

            if (Category != null)
            {
                var context = _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == id);
                var articles = context.ToList();
                foreach(Article article in articles)
                {                  
                    if (article.imagePath is not null)
                    {
                        string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                        string filePath = Path.Combine(uploadFolder, article.imagePath);
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                    }
                    _context.Articles.Remove(article);
                }
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

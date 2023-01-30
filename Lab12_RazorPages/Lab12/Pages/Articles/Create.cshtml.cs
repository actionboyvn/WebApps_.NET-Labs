using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab12.Data;
using Lab12.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lab12.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly Lab12.Data.MyDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;


        public CreateModel(Lab12.Data.MyDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Article.CategoryId is null)
            {
                return Page();
            }
            string imageName = null;
            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
            if (Article.FormFile != null)
            {
                imageName = Guid.NewGuid().ToString() + "_" + Article.FormFile.FileName;
                string filePath = Path.Combine(uploadFolder, imageName);
                FileStream p = new FileStream(filePath, FileMode.Create);
                Article.FormFile.CopyTo(p);
                p.Dispose();
                Article.imagePath = imageName;
            }
            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

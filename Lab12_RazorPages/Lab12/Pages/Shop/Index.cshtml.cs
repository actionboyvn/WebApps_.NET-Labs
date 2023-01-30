using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab12.Data;
using Lab12.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Lab12.Pages.Shop
{
    public class IndexModel : PageModel
    {
        private readonly Lab12.Data.MyDbContext _context;

        public IndexModel(Lab12.Data.MyDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty(SupportsGet = true)]
        [Display(Name ="Choose a category:")]
        public int? Id { get; set; }
        public void OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");            
        }
    }
}

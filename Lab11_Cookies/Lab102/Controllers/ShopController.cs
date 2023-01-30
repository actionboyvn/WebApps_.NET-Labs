using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab102.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            if (Id is null)
                return RedirectToAction(nameof(Index));
            var context = _context.Articles
                .Include(a => a.Category)
                .Where(a => a.CategoryId == Id);           
            ViewData["Category"] = "Category: " + _context.Categories.First(c => c.Id == Id).Name;          
            return View(await context.ToListAsync());
        }
        public IActionResult Cart()
        {
            Dictionary<int, int> quantities = getQuantities();
            ViewBag.nums = quantities;
            ViewBag.totalCost = calcTotalCost();
            var context = _context.Articles.
                Include(a => a.Category).
                Where(a => quantities.Keys.Contains(a.Id));             
            return View(context);
        }

        public Dictionary<int, int> getQuantities()
        {
            string? temp;
            Request.Cookies.TryGetValue("quantities", out temp);
            if (temp is not null)
            {
                return JsonConvert.DeserializeObject<Dictionary<int, int>>(Request.Cookies["quantities"]);
            }
            else
                return new Dictionary<int, int>();
        }
        public float calcTotalCost()
        {            
            Dictionary<int, int> quantities = getQuantities();
            var articles = _context.Articles.ToList();            
            float totalCost = 0;                                  
            for (int i = 0; i < articles.Count(); i++)
            {
                if (quantities.ContainsKey(articles[i].Id))
                    totalCost += quantities[articles[i].Id] * articles[i].Price;
            }
            return totalCost;
        }
        public void saveQuantities(Dictionary<int, int> quantities)
        {
            string temp = JsonConvert.SerializeObject(quantities);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);          
            Response.Cookies.Append("quantities", temp, options);
        }

        public IActionResult AddToCart(int id)
        {
            Dictionary<int, int> quantities = getQuantities();
            if (quantities.ContainsKey(id))
                quantities[id]++;
            else
                quantities.Add(id, 1);
            saveQuantities(quantities);            
            return RedirectToAction(nameof(Cart));
        }
   
        public IActionResult ReduceFromCart(int id)
        {
            Dictionary<int, int> quantities = getQuantities();
            if (quantities.ContainsKey(id))
            {
                quantities[id]--;
                if (quantities[id] == 0)
                    quantities.Remove(id);
            }
            saveQuantities(quantities);
            return RedirectToAction(nameof(Cart));
        }
    }
}

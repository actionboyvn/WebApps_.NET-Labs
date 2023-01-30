using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab08.Controllers
{
    public class ToolController : Controller
    {        
        public IActionResult Solve(double a, double b, double c)
        {            
            ViewBag.color = "cyan";
            ViewBag.res = "";
            double delta = b * b - 4 * a * c;
            if (a == 0 && b == 0)
            {
                if (c == 0)
                    ViewBag.res = "Identity.";
                else
                {
                    ViewBag.res = "No solution.";
                    ViewBag.color = "red";
                }
                return View();
            }
            if (a == 0)
            {
                ViewBag.res = String.Format("Sol = {0:0.00}", -c / b);
                ViewBag.color = "pink";
                return View();
            }
            if (delta == 0)
            {
                ViewBag.res = String.Format("Sol1 = Sol2 = {0:0.00}", -b / 2 / a);
                ViewBag.color = "blue";                
            }
            if (delta > 0)
            {
                ViewBag.res = String.Format("Sol1 = {0:0.00}, Sol2 = {1:0.00}", 
                    (-b - Math.Sqrt(delta)) / 2 / a, (-b + Math.Sqrt(delta)) / 2 / a);                
                ViewBag.color = "green";                
            }
            if (delta < 0)
            {
                ViewBag.res = "No solution.";
                ViewBag.color = "red";
            }
            return View();
        }
    }
}

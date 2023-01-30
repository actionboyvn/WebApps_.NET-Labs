using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab08.Controllers
{
    public class GameController : Controller
    {
        static readonly Random random = new Random();
        static int n = 0;
        static int randValue = 0;
        static int tries = 0;        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Set(int newRange)
        {
            n = newRange;
            ViewBag.n = n;
            return View("Game");
        }
        public IActionResult Draw()
        {
            ViewBag.n = n;
            randValue = random.Next(0, n);
            tries = 0;
            if (n == 0)
                ViewBag.sms = "You must set the range first!";
            else
                ViewBag.sms = "A new random number has been drawn. You can start the game!";
            ViewBag.color = "blue";
            return View("Game");
        }
        public IActionResult Guess(int x)
        {
            ViewBag.n = n;
            tries++;
            if (x == randValue)
            {
                ViewBag.sms = String.Format("You have guessed it right after {0} tries", tries);
                ViewBag.color = "blue";
            }
            if (x < randValue)
            {
                ViewBag.sms = String.Format("Try it again. You guessed too small! You have guessed {0} times.", tries);
                ViewBag.color = "green";
            }
            if (x > randValue)
            {
                ViewBag.sms = String.Format("Try it again. You guessed too big! You have guessed {0} times.", tries);
                ViewBag.color = "red";
            }
            return View("Game");
        }
    }
}

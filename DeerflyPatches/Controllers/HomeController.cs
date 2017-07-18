using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeerflyPatches.Models;
using Microsoft.EntityFrameworkCore;

namespace DeerflyPatches.Controllers
{
    public class HomeController : Controller
    {
        private readonly DeerflyPatchesContext _context;

        public HomeController(DeerflyPatchesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Order()
        {
            return View(await _context.Product.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

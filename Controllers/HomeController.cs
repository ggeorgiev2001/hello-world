using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqlQueryApp.Data;

namespace SqlQueryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
            
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> Statistics()
        {
            var stats = await _context.Products
                .Where(p => p.IsActive)
                .GroupBy(p => 1)
                .Select(g => new
                {
                    TotalProducts = g.Count(),
                    TotalValue = g.Sum(p => p.Price * p.Stock),
                    AveragePrice = g.Average(p => p.Price),
                    MinPrice = g.Min(p => p.Price),
                    MaxPrice = g.Max(p => p.Price),
                    TotalStock = g.Sum(p => p.Stock)
                })
                .FirstOrDefaultAsync();

            return View(stats);
        }
    }
}
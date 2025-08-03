using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SqlQueryApp.Data;
using SqlQueryApp.Models;

namespace SqlQueryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(
            [FromQuery] string? search = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] bool? isActive = null)
        {
            var query = _context.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }

            return await query.OrderBy(p => p.Name).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Products/search-by-price-range
        [HttpGet("search-by-price-range")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchByPriceRange(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice)
        {
            var products = await _context.Products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.IsActive)
                .OrderBy(p => p.Price)
                .ToListAsync();

            return products;
        }

        // GET: api/Products/low-stock
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLowStockProducts([FromQuery] int threshold = 10)
        {
            var products = await _context.Products
                .Where(p => p.Stock <= threshold && p.IsActive)
                .OrderBy(p => p.Stock)
                .ToListAsync();

            return products;
        }

        // GET: api/Products/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetProductStatistics()
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

            return Ok(stats);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StocksAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stocks>>> GetStocks()
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            return await _context.Stocks.ToListAsync();
        }

        // GET: api/Stocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stocks>> GetStocks(Guid id)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stocks = await _context.Stocks.FindAsync(id);

            if (stocks == null)
            {
                return NotFound();
            }

            return stocks;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStocks(Guid id, Stocks stocks)
        {
            if (id != stocks.StockId)
            {
                return BadRequest();
            }

            _context.Entry(stocks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StocksExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stocks>> PostStocks(Stocks stocks)
        {
            if (_context.Stocks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stocks'  is null.");
            }
            _context.Stocks.Add(stocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStocks", new { id = stocks.StockId }, stocks);
        }

        // DELETE: api/Stocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStocks(Guid id)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stocks = await _context.Stocks.FindAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stocks);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StocksExists(Guid id)
        {
            return (_context.Stocks?.Any(e => e.StockId == id)).GetValueOrDefault();
        }
    }
}

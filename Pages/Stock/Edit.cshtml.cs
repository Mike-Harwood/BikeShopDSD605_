using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Pages.Stock
{
    [Authorize(Policy = "EditStockPolicy")]
    public class EditModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public EditModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Stocks Stocks { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks.FirstOrDefaultAsync(m => m.StockId == id);
            if (stocks == null)
            {
                return NotFound();
            }
            Stocks = stocks;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Stocks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StocksExists(Stocks.StockId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StocksExists(Guid? stockId)
        {
            throw new NotImplementedException();
        }

        private bool StocksExists(Guid id)
        {
            return (_context.Stocks?.Any(e => e.StockId == id)).GetValueOrDefault();
        }
    }
}

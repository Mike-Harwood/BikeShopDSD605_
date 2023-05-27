using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Stock
{
    public class DetailsModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public DetailsModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            else 
            {
                Stocks = stocks;
            }
            return Page();
        }
    }
}

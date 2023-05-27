using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Order
{
    public class CreateModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public CreateModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
        ViewData["StaffId"] = new SelectList(_context.Set<Staffs>(), "StaffId", "StaffId");
        ViewData["StockId"] = new SelectList(_context.Set<Stocks>(), "StockId", "StockId");
            return Page();
        }

        [BindProperty]
        public Orders Orders { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Orders == null || Orders == null)
            {
                return Page();
            }

            _context.Orders.Add(Orders);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

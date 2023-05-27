using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public DetailsModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Orders Orders { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }
            else 
            {
                Orders = orders;
            }
            return Page();
        }
    }
}

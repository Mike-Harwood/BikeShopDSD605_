using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Pages.Order
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public IndexModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Orders> Orders { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Stock).ToListAsync();
            }
        }
    }
}

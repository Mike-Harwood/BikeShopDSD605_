using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Pages.Staff
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public IndexModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Staffs> Staffs { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Staffs != null)
            {
                Staffs = await _context.Staffs.ToListAsync();
            }
        }
    }
}

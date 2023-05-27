using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public IndexModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Staffs> Staffs { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Staffs != null)
            {
                Staffs = await _context.Staffs.ToListAsync();
            }
        }
    }
}

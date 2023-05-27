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
    public class DetailsModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public DetailsModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Staffs Staffs { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs.FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }
            else 
            {
                Staffs = staffs;
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly BikeShopDSD605.Data.ApplicationDbContext _context;

        public EditModel(BikeShopDSD605.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staffs Staffs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Staffs == null)
            {
                return NotFound();
            }

            var staffs =  await _context.Staffs.FirstOrDefaultAsync(m => m.StaffId == id);
            if (staffs == null)
            {
                return NotFound();
            }
            Staffs = staffs;
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

            _context.Attach(Staffs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffsExists(Staffs.StaffId))
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

        private bool StaffsExists(Guid id)
        {
          return (_context.Staffs?.Any(e => e.StaffId == id)).GetValueOrDefault();
        }
    }
}

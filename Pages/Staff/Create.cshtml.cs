using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BikeShopDSD605.Data;
using BikeShopDSD605.Models;

namespace BikeShopDSD605.Pages.Staff
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
            return Page();
        }

        [BindProperty]
        public Staffs Staffs { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Staffs == null || Staffs == null)
            {
                return Page();
            }

            _context.Staffs.Add(Staffs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

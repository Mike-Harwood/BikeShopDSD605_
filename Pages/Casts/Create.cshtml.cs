﻿using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BikeShopDSD605.Pages.Casts
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
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Cast Cast { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Cast == null || Cast == null)
            {
                return Page();
            }

            _context.Cast.Add(Cast);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

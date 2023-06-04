using BikeShopDSD605.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BikeShopDSD605.Pages.Stock
{
    [Authorize(Policy = "CreateStockOver18Policy")]
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
        public Stocks Stocks { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Stocks == null || Stocks == null)
            {
                return Page();
            }

            _context.Stocks.Add(Stocks);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

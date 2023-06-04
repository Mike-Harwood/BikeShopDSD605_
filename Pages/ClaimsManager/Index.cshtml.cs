using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BikeShopDSD605.Pages.ClaimsManager
{
    //[Authorize(Policy = "ViewClaimPolicy")]


    [BindProperties]


    public class IndexModel : PageModel
    {
        public UserManager<IdentityUser> _UserManager { get; set; }
        public IndexModel(UserManager<IdentityUser> userManager)
        {
            _UserManager = userManager;
        }

        public List<IdentityUser> Users { get; set; }
        public async Task OnGetAsync()

        {
            Users = await _UserManager.Users.ToListAsync();
        }

    }
}
using BikeShopDSD605.Data;
using BikeShopDSD605.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BikeShopDSD605.Pages.RoleManager
{

    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        //created a list of all the Roles
        public List<IdentityRole> Roles { get; set; }

        //create a list of all the Current Users and Roles
        public List<UserRoles> UserAndRoles { get; set; }


        //create the Users and Roles from the DataBase
        public List<UserRoles> GetUserAndRoles()
        {
            var list = (from user in _context.Users
                        join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                        join role in _context.Roles on userRoles.RoleId equals role.Id
                        select new UserRoles { UserName = user.UserName, RoleName = role.Name }).ToList();
            return list;
        }

        public void OnGet()
        {
            //passing the Roles to the front end
            Roles = _roleManager.Roles.ToList();
            //Passing the users and roles to the front end
            UserAndRoles = GetUserAndRoles();
        }
    }
}

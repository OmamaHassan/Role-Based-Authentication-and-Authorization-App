using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RoleBasedAccessControl.Pages
{
    [Authorize(Roles ="admin")]
    public class ClientModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public ClientModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager; 
        }

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public async Task<IActionResult> OnGetAsync()
        {

            var users = await userManager.GetUsersInRoleAsync("client");
            Users = users.ToList();

            return Page(); 
        }

    }
}
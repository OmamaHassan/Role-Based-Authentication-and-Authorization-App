using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoleBasedAccessControl.Pages
{
    [Authorize(Roles ="admin")]
    public class EmployeeModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public EmployeeModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IList<IdentityUser> Users { get; set; } = new List<IdentityUser>();

        public async Task<IActionResult> OnGetAsync()
        {
            var users = await userManager.GetUsersInRoleAsync("employee");
            Users = users.ToList();

            return Page();
        }
  
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RoleBasedAccessControl.Pages
{
    [Authorize(Roles ="admin")]
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public DeleteUserModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public required string userEmail { get; set; }

        public async Task<IActionResult> OnPostAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);

            if (user is not null)
            {
                await userManager.DeleteAsync(user);
                TempData["Message"] = userEmail + " Deleted!";
            }

            else    
                TempData["Message"] = userEmail + " Not Found";
       
            return RedirectToPage();
        }

    }
}

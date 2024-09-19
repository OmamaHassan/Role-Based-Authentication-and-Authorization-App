using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace RoleBasedAccessControl.Pages
{
    [Authorize(Roles = "admin")]
    public class AssignRoleModel : PageModel
    {

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AssignRoleModel(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public required string userEmail { get; set; }

        [BindProperty]
        public required string SelectedRole { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user is not null)
            {
                var currentRoles = await userManager.GetRolesAsync(user);

                foreach (var role in currentRoles)
                {
                    var removeRole = await userManager.RemoveFromRoleAsync(user, role);
                }
                await userManager.AddToRoleAsync(user, SelectedRole);
                TempData["Message"] =$"Role '{ SelectedRole }' Assigned To '{ userEmail }'";
            }

            else
            {
                TempData["Message"] = userEmail + " Not Found";
            }
            
            return RedirectToPage();
        }

        
        public List<SelectListItem> Roles { get; private set; } = new List<SelectListItem>();
        
        public async Task<IActionResult> OnGetAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            Roles = roles.Select(role => new SelectListItem
            {
                Value = role.Name,
                Text = role.Name
            }).ToList();

            return Page();
        }
    }
}
    


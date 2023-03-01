using System.ComponentModel.DataAnnotations;
using Identity_Sample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Identity_Sample.Areas.Admin.Pages.Role
{
    public class AddUserRole : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AddUserRole(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public class InputModel
        {
            [Required] public string ID { get; set; }
            public string? Name { get; set; }
            public string[] RoleNames { get; set; }
        }

        [BindProperty] public InputModel Input { get; set; }
        [BindProperty] public bool isConfirmed { get; set; }
        [TempData] public string StatusMessage { get; set; }

        public IActionResult OnGet() => NotFound("Not Found");

        public List<string> AllRoles { get; set; } = new List<string>();

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByIdAsync(Input.ID);
            if (user == null)
            {
                return NotFound("Cannot find role to delete.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.ToListAsync();
            allRoles.ForEach(r => { AllRoles.Add(r.Name); });
            if (!isConfirmed)
            {
                Input.RoleNames = roles.ToArray();
                isConfirmed = true;
                StatusMessage = "";
                ModelState.Clear();
            }
            else
            {
                StatusMessage = "Updated";
                if (Input.RoleNames == null)
                {
                    Input.RoleNames = new string[] { };
                }
                foreach (var roleName in Input.RoleNames)
                {
                    if (roles.Contains(roleName)) continue;
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                foreach (var roleName in roles)
                {
                    if (Input.RoleNames.Contains(roleName)) continue;
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            Input.Name = user.UserName;
            return Page();
        }
    }
}
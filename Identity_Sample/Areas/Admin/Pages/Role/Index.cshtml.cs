using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Identity_Sample.Areas.Admin.Pages.Role
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public List<IdentityRole> roles { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGet()
        {
            roles = await _roleManager.Roles.ToListAsync();
            return Page();
        }
    }
}
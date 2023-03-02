using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Identity_Sample.Data;

namespace Identity_Sample.Areas.Admin.Pages.RoleClaims
{
    public class CreateModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityRole role { set; get; }

        public CreateModel(AppDBContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        async Task<IdentityRole> GetRole()
        {
            if (string.IsNullOrEmpty(roleid)) return null;
            return await _roleManager.FindByIdAsync(roleid);
        }

        public async Task<IActionResult> OnGet()
        {
            role = await GetRole();
            if (role == null)
                return NotFound("Not found role");
            return Page();
        }



        [BindProperty(SupportsGet = true)]
        public string roleid { set; get; }

        [BindProperty]
        public IdentityRoleClaim<string> EditClaim { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            role = await GetRole();
            if (role == null)
                return NotFound("Not Found Role");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            EditClaim.RoleId = roleid;

            _context.RoleClaims.Add(EditClaim);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { roleid = roleid });
        }
    }
}
using Identity_Sample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Identity_Sample.Areas.Admin.Pages.Role
{
    public class UserModel : PageModel
    {
        const int USERS_PER_PAGE = 10;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public UserModel(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public class UserInList : AppUser
        {
            public string listRoles { get; set; }
        }

        public List<UserInList> users;
        public int totalPages { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public int pageNumber { get; set; }

        public IActionResult OnPost() => NotFound("Not allowed");
        public async Task<IActionResult> OnGet()
        {
            var cuser = await _userManager.GetUserAsync(User);
            // await _userManager.AddToRolesAsync(cuser, new string[] { "Role02" });
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            var lusers = (from u in _userManager.Users
                          orderby u.UserName
                          select new UserInList()
                          {
                              Id = u.Id,
                              UserName = u.UserName
                          });
            int totalUsers = await lusers.CountAsync();
            totalPages = (int)Math.Ceiling((double)totalUsers / USERS_PER_PAGE);
            users = await lusers.Skip(USERS_PER_PAGE * (pageNumber - 1)).Take(USERS_PER_PAGE).ToListAsync();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.listRoles = string.Join(",", roles.ToList());
            }
            return Page();
        }
    }
}
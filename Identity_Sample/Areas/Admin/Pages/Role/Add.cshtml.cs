using System.ComponentModel.DataAnnotations;
using Identity_Sample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Areas.Admin.Pages.Role
{
    public class AddModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDBContext _appDBContext;
        public AddModel(RoleManager<IdentityRole> roleManager, AppDBContext appDBContext)
        {
            _roleManager = roleManager;
            _appDBContext = appDBContext;
        }
        [TempData]
        public string StatusMessage { get; set; }
        public class InputModel
        {
            public string ID { get; set; }
            [Required(ErrorMessage = "Please enter role name")]
            [Display(Name = "Role Name")]
            [StringLength(100, ErrorMessage = "{0} must be {2} and {1} characters long.", MinimumLength = 3)]
            public string Name { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public bool IsUpdate { get; set; }

        public IActionResult OnGet() => NotFound("Not Found");
        public IActionResult OnPost() => NotFound("Not Found");

        public IActionResult OnPostStartNewRole()
        {
            StatusMessage = "Please enter new role information";
            IsUpdate = false;
            ModelState.Clear();
            return Page();
        }

        public async Task<IActionResult> OnPostStartUpdate()
        {
            StatusMessage = null;
            IsUpdate = true;
            if (Input.ID == null)
            {
                StatusMessage = "Error: No information about role";
                return Page();
            }
            var result = await _roleManager.FindByIdAsync(Input.ID);
            if (result != null)
            {
                Input.Name = result.Name;
                ViewData["Title"] = "Update role " + Input.Name;
                ModelState.Clear();
            }
            else
            {
                StatusMessage = "Error: No information about role ID " + Input.ID;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAddOrUpdate()
        {

            if (!ModelState.IsValid)
            {
                StatusMessage = null;
                return Page();
            }
            if (IsUpdate)
            {
                if (Input.ID == null)
                {
                    ModelState.Clear();
                    StatusMessage = "Error: No information about role";
                    return Page();
                }
                var result = await _roleManager.FindByIdAsync(Input.ID);
                if (result != null)
                {
                    result.Name = Input.Name;
                    var roleUpdateRs = await _roleManager.UpdateAsync(result);
                    if (roleUpdateRs.Succeeded)
                    {
                        StatusMessage = "Update role successfully.";
                    }
                    else
                    {
                        StatusMessage = "Error: ";
                        foreach (var er in roleUpdateRs.Errors)
                        {
                            StatusMessage += er.Description;
                        }
                    }
                }
                else
                {
                    StatusMessage = "Error: Cannot find role to update.";
                }
            }
            else
            {
                var newRole = new IdentityRole(Input.Name);
                newRole.Id = Guid.NewGuid().ToString();
                var rsNewRole = await _roleManager.CreateAsync(newRole);
                if (rsNewRole.Succeeded)
                {
                    StatusMessage = $"Created role {newRole.Name} successfully.";
                    return RedirectToPage("./Index");
                }
                else
                {
                    StatusMessage = "Error: ";
                    foreach (var er in rsNewRole.Errors)
                    {
                        StatusMessage += er.Description;
                    }
                }
            }
            return Page();
        }
    }
}
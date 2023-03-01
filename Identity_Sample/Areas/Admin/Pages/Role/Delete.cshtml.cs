using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Areas.Admin.Pages.Role
{
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public DeleteModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public class InputModel
        {
            [Required]
            public string? ID { get; set; }
            public string? Name { get; set; }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        public bool isConfirmed { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet() => NotFound("Not Found");
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Cannot delete role.");
            }
            var role = await _roleManager.FindByIdAsync(Input.ID);
            if (role == null)
            {
                return NotFound("Cannot find role to delete.");
            }
            ModelState.Clear();
            if (isConfirmed)
            {
                await _roleManager.DeleteAsync(role);
                StatusMessage = "Deleted " + role.Name;
                return RedirectToPage("Index");
            }
            else
            {
                Input.Name = role.Name;
                isConfirmed = true;
            }
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Pages
{
    [Authorize(Roles = "Role01")]
    public class TestAuthorize1Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
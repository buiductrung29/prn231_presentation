using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Pages
{
    [Authorize(Policy = "CanEdit")]
    public class TestAuthorize3Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
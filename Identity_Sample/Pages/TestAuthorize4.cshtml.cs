using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Pages
{
    [Authorize(Policy = "CanView")]
    public class TestAuthorize4Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity_Sample.Pages
{
    [Authorize(Roles = "Role02")]
    [Authorize(Roles = "Role03")]
    public class TestAuthorize2Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}
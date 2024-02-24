using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.Helpers.Constants;

namespace Razor_UTC.Pages.Authentication
{
    public class SignOutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync(Constants.cookies);
            return RedirectToPage("/Index");
        }
    }
}

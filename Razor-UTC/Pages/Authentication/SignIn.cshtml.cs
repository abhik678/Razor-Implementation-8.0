using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.Helpers.Constants;
using Razor_UTC.Models;
using System.Security.Claims;

namespace Razor_UTC.Authentication
{
    public class AuthenticationModel : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) { return Page(); }

            if(Credentials.Username == "Abhitosh" &&  Credentials.Password == "Password")
            {
                var claim = new List<Claim>()
                {
                    new(ClaimTypes.Name, Credentials.Username),
                    new("Password", Credentials.Password)
                };
                ClaimsIdentity identity = new(claim, Constants.cookies);
                ClaimsPrincipal principal = new(identity);
                await HttpContext.SignInAsync(Constants.cookies, principal);

                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}

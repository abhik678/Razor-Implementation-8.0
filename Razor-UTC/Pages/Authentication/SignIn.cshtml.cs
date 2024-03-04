using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_UTC.DBContext;
using Razor_UTC.Helpers.Constants;
using Razor_UTC.Models;
using System.Security.Claims;

namespace Razor_UTC.Authentication
{
    public class AuthenticationModel(UserDbContext userDbContext) : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; } = default!;

        public UserDbContext UserDbContext { get; set; } = userDbContext;
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) { return Page(); }
            Registration? user = await UserDbContext.Registrations.SingleOrDefaultAsync(x => x.Username.Equals(Credentials.Username) && x.Password.Equals(Credentials.Password));
            if (user != null)
            {
                var claim = new List<Claim>()
                {
                    new(ClaimTypes.Name, Credentials.Username),
                    new("Password", Credentials.Password),
                    new("Admin", "User")
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

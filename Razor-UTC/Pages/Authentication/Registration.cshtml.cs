using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.DBContext;
using Razor_UTC.Helpers.Constants;
using Razor_UTC.Models;
using System.Security.Claims;

namespace Razor_UTC.Pages.Authentication
{
    public class RegistrationModel(UserDbContext dbContext) : PageModel
    {
        [BindProperty]
        public Registration Registration { get; set; } = default!;
        public UserDbContext DbContext { get; set; } = dbContext;

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) { return Page(); }


            if(Registration.Username != null && Registration.Password != null && Registration.ConfirmPassword != null)
            {
                await DbContext.Registrations.AddAsync(Registration);
                await DbContext.SaveChangesAsync();

                var claims = new List<Claim>()
                {
                    new(ClaimTypes.Name, Registration.Username),
                    new(ClaimTypes.Authentication, Registration.Password),
                    new(ClaimTypes.Authentication, Registration.ConfirmPassword)
                };
                ClaimsIdentity identity = new(claims, Constants.cookies);
                ClaimsPrincipal principal = new(identity);
                await HttpContext.SignInAsync(Constants.cookies, principal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.DBContext;
using Razor_UTC.Models;

namespace Razor_UTC.Pages.ContactUs
{
    [Authorize(Policy = "Authorized")]
    public class ContactModel(UserDbContext context) : PageModel
    {
        [BindProperty]
        public UserInformation Users { get; set; } = default!;
        public UserDbContext Context { get; set; } = context;
        public string Username { get; private set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            if(Users.Id > 0 && !string.IsNullOrEmpty(Users.FName) && !string.IsNullOrEmpty(Users.LName) 
                && !string.IsNullOrEmpty(Users.EmailAddress) && !string.IsNullOrEmpty(Users.Password) 
                && !string.IsNullOrEmpty(Users.ConfirmPassword) && Users.PhoneNumber > 0)
            {
                Users.Id = 0;
                Context.UsersInformation.Add(Users);
                await Context.SaveChangesAsync();

                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnGet()
        {
            Username = HttpContext.User.Identity.Name;
            return Page();
        }
    }
}

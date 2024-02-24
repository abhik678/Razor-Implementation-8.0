using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.DBContext;
using Razor_UTC.Models;

namespace Razor_UTC.Pages.ContactUs
{
    [AllowAnonymous]
    public class ContactModel(UserDbContext context) : PageModel
    {
        [BindProperty]
        public UserInformation Users { get; set; } = default!;
        public UserDbContext Context { get; set; } = context;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            if(Context != null || Users != null || Context!.UsersInformation != null) 
            {
                await Context!.UsersInformation.AddAsync(Users!);
                await Context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }
    }
}

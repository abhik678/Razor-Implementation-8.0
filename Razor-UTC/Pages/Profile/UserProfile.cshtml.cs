using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_UTC.DBContext;
using Razor_UTC.Models;

namespace Razor_UTC.Pages.Profile
{
    public class UserProfileModel(UserDbContext context) : PageModel
    {
        [BindProperty]
        public UserInformation? userInformation { get; set; } = default!;

        public UserDbContext Context = context;
        public async Task<IActionResult> OnGetAsync()
        {
            userInformation = await Context.UsersInformation.FirstOrDefaultAsync().ConfigureAwait(false);
            if (userInformation == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

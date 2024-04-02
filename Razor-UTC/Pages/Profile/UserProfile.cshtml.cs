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
        public UserInformation userInformation = default!;

        public UserDbContext Context = context;
        /*public async Task<IActionResult> OnGetAsync(string email)
        {
            if(Context != null)
            {
                var response = await Context.UsersInformation.FirstOrDefaultAsync(x => x.EmailAddress == email);
            }
            return RedirectToAction("DisplayUserProfile");
        }*/

        private Task<IActionResult> Ok(string v)
        {
            throw new NotImplementedException();
        }
    }
}

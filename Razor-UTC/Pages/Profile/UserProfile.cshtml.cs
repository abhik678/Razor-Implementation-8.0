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
        public async Task<IActionResult> OnGetAsync()
        {
            if(Context != null)
            {
                var response = await Context.UsersInformation.ToListAsync();
                return new JsonResult(response);

            }
            return await Ok("200");
        }

        private Task<IActionResult> Ok(string v)
        {
            throw new NotImplementedException();
        }
    }
}

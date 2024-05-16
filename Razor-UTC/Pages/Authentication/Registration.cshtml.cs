using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.DBContext;
using User.IdentityServer.Shared.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Razor_UTC.Pages.Authentication
{
    public class RegistrationModel(UserDbContext dbContext, IHttpClientFactory HttpClientFactory) : PageModel
    {
        [BindProperty]
        public Registration Registration { get; set; } = default!;
        public UserDbContext DbContext { get; set; } = dbContext;
        public HttpClient HttpClient { get; set; } = HttpClientFactory.CreateClient("clientlogin");

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            StringContent stringContent = new(JsonConvert.SerializeObject(Registration), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await HttpClient.PostAsync($"api/Users/" + "RegisterUserAsync", stringContent).ConfigureAwait(false);

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}

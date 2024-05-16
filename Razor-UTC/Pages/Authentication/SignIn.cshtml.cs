using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_UTC.DBContext;
using User.IdentityServer.Shared.Models;
using System.Security.Claims;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Razor_UTC.Authentication
{
    public class AuthenticationModel(UserDbContext userDbContext, IHttpClientFactory httpClientFactory) : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; } = default!;

        public UserDbContext UserDbContext { get; set; } = userDbContext;

        public HttpClient _httpClient { get; set; } = httpClientFactory.CreateClient("clientlogin");

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string inputContent = JsonConvert.SerializeObject(Credentials);

            StringContent content = new(inputContent, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"api/Users", content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                var resultToken = JsonConvert.DeserializeObject<JwtAccessToken>(token);

                if (!string.IsNullOrEmpty(resultToken!.AccessToken))
                {
                    List<Claim> claims = [];
                    claims = [new("IdentityToken", resultToken.AccessToken), new(ClaimTypes.Name, Credentials.Username)];
                    ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new(identity);

                    AuthenticationProperties auth = new()
                    {
                        IsPersistent = true,
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth).ConfigureAwait(false);
                }

                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;
using Razor_UTC.DBContext;
using Serilog;
using Serilog.Core;
using Constants = Razor_UTC.Helpers.Constants.Constants;
using IHostEnvironment = Microsoft.Extensions.Hosting.IHostEnvironment;

namespace Razor_UTC
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; set; } = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorPages();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = Constants.cookies;
            });

            IServiceCollection res = services.AddAuthorization(options =>
            {
                options.AddPolicy("Authorized", options => { options.RequireClaim("Admin", "User"); });
                options.AddPolicy("UnAuthorized", options => { options.RequireClaim("Guest", "User"); });
            });

            services.AddDbContext<UserDbContext>();

            string path = Configuration.GetSection("Logging:LogPath:logPath").Value!;

            Logger logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel
                .Override("microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext().WriteTo.File(path!).CreateLogger();
            services.AddHttpClient("clientlogin", c =>
            {
                c.BaseAddress = Constants.GetUri;
            });
            services.AddCors(c =>
            {
                c.AddPolicy("usecors", build => { build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("usecors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            { endpoints.MapRazorPages(); });
        }
    }
}
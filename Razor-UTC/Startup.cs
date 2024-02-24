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
            services.AddAuthentication(Constants.cookies).AddCookie(Constants.cookies, options =>
            {
                options.Cookie.Name =   Constants.cookies;
            });
            services.AddAuthorizationBuilder()
                .AddPolicy("Enrolled", option => { option.RequireClaim("Authorized", "User"); })
                .AddPolicy("UnEnrolled", option => { option.RequireClaim("Guest", "User"); });
            services.AddDbContext<UserDbContext>();

            string path = Configuration.GetSection("Logging:LogPath:logPath").Value!;

            Logger logger = new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel
                .Override("microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext().WriteTo.File(path!).CreateLogger();
            
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => 
            { endpoints.MapRazorPages(); });
        }
    }
}

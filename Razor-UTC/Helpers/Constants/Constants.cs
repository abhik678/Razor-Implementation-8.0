namespace Razor_UTC.Helpers.Constants
{
    public static class Constants
    {
        public const string cookies = "Cookies";
        public const string CookiesName = "MyCookies";
        public const string ConnectionString = "MyCookies";
        public const string EnrollPolicyName = "Authorized";
        public const string UnEnrollPolicyName = "Guest";
        public static Uri GetUri { get; set; } = new Uri("https://localhost:44398/");
    }
}

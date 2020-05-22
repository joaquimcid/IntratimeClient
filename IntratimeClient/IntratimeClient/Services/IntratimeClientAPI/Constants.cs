namespace IntratimeClient.Services.IntratimeClientAPI
{
    public static class Constants
    {
        public const string UrlBase = @"http://newapi.intratime.es/api/user";

        public const string UrlLogin = @"/login";

        public const string UrlClocking = @"/clocking";

        public const string UrlGetClocking = @"/clockings";

        public static string FullUrl(this string path) => UrlBase + path;
    }
}

namespace CompanionApp.Extensions
{
    public static class HttpContextExtenions
    {
        public static string GeUserID(this HttpContext httpContext)
        {
            if (httpContext is null)
            {
                return string.Empty;
            }
            return httpContext.User.Claims.Single(c => c.Type == "userID").Value;
        }
    }
}

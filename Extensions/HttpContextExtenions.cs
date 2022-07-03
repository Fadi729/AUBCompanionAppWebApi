namespace CompanionApp.Extensions
{
    public static class HttpContextExtenions
    {
        public static Guid GetUserID(this HttpContext httpContext)
        {
            if (httpContext is null)
            {
                return Guid.Empty;
            }
            return Guid.Parse(httpContext.User.Claims.Single(c => c.Type == "userID").Value);
        }
    }
}

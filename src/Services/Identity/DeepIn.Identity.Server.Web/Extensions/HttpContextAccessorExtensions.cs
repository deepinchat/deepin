namespace DeepIn.Identity.Server.Web.Extensions;

public static class HttpContextAccessorExtensions
{
    public static string GetIpAddress(this IHttpContextAccessor httpContextAccessor)
    {
        var ip = httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ip))
        {
            ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
        return ip;
    }
}

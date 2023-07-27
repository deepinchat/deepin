namespace DeepIn.Identity.Server.Web.Configurations
{
    public class RoutersConfig
    {
        public static string ConfirmEmail(string userId, string returnUrl) => $"/confirm-email?id={userId}&returnUrl={returnUrl ?? "/"}";
        public static string LoginWithTwoFactor(bool remberMe, string returnUrl) => $"/login-with-2factor?remberMe={remberMe}&returnUrl={returnUrl ?? "/"}";
        public static string Lockout => "/lockout";
    }
}

namespace DeepIn.Identity.Server.Web.Models.Account
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(28);

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = false;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups = false;
         
        public static string EmailIsTakenErrorMessage = "EmailAlreadyTaken";
        public static string InvalidExternalLoginErrorMessage = "InvalidExternalLogin";
        public static ModelErrorMessage InvalidCredentials = new ModelErrorMessage("invalid_credentials", "Invalid Credentials");
        public static ModelErrorMessage InvalidAuthenticatorCode = new ModelErrorMessage("invalid_authenticator_code", "Invalid Authenticator Code");
    }
    public class ModelErrorMessage
    {
        public string Key { get; set; }
        public string Message { get; set; }
        public ModelErrorMessage(string key, string message = null)
        {
            Key = key;
            Message = message;
        }
    }
}

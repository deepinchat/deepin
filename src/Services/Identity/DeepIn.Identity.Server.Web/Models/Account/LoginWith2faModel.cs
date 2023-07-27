namespace DeepIn.Identity.Server.Web.Models.Account
{
    public class LoginWith2faModel
    {
        public string TwoFactorCode { get; set; }
        public bool rememberLogin { get; set; }
        public bool RememberMachine { get; set; }
    }
}

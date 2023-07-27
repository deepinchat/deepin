using System.ComponentModel.DataAnnotations;

namespace DeepIn.Identity.Server.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [StringLength(32, ErrorMessage = "The password must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
    }
    public class LoginConfigModel
    {
        public bool AllowRememberLogin { get; set; } = true;
        public bool EnableLocalLogin { get; set; } = true;
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}

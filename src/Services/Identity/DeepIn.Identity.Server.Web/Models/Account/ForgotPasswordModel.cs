using System.ComponentModel.DataAnnotations;

namespace DeepIn.Identity.Server.Web.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

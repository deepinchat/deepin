using System.ComponentModel.DataAnnotations;

namespace DeepIn.Identity.Server.Web.Models.Account
{
    public class ResetPasswordModel : ForgotPasswordModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "The password must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(6, ErrorMessage = "The verify code must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Code { get; set; }
    }
}

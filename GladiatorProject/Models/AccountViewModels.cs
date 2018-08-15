using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GladiatorProject.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [RegularExpression(@"^[^<>,?;:'()!~%\-_#/*""\s]+$")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [RegularExpression(@"^[^<>,?;:'()!~%\-_#/*""\s]+$")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]  
        [Display(Name = "User Name")] // changed the login to username instead of emails.  Need to make a check so not 2 users will have same name.
        [MaxLength(25)]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$")]  //stopping this symbols from being used when typing in your user name.
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [MaxLength(25)]
        [RegularExpression(@"^[^<>.,?;:'()!~%\-_@#/*""\s]+$")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^<>,?;:'()!~%\-_#/*""\s]+$")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

        public List<string> Roles { get; set; }

        public RegisterViewModel()
        {
            Roles = new List<string>();
            Roles.Add("Admin");
            Roles.Add("Support");
            Roles.Add("Player");
        }
    }
    

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^<>,?;:'()!~%\-_#/*""\s]+$")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^<>,?;:'()!~%\-_#/*""\s]+$")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

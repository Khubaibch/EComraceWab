using System.ComponentModel.DataAnnotations;
namespace EComraceWab.Models
{


    public class AuthenticationRequist
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The Password is required")]
        public string Password { get; set; }
    }

}

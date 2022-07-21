using System.ComponentModel.DataAnnotations;

namespace SajhaSabal.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
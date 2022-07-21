using System.ComponentModel.DataAnnotations;

namespace SajhaSabal.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Authorization
{
    public class AuthDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

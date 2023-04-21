using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Authorization
{
    public class TokenDto
    {
        [Required]
        public string? AccessToken { get; set; }
        [Required]
        public string? RefreshToken { get; set;}
    }
}

using System.ComponentModel.DataAnnotations;

namespace Zawamil.Web.Models.Users.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

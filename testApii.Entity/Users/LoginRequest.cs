using System.ComponentModel.DataAnnotations;

namespace testApii.Entity.Users
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

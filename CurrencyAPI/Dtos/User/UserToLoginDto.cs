using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.User
{
    public class UserToLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
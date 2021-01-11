using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.User
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int MainCurrencyId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
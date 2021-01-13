using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.User
{
    /// <summary>
    ///
    /// </summary>
    public class RegisterUserDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public int MainCurrencyId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Role { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
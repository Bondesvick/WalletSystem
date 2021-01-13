using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.User
{
    /// <summary>
    ///
    /// </summary>
    public class UserToLoginDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
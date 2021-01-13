using System.ComponentModel.DataAnnotations;

namespace WalletSystemAPI.Dtos.User
{
    /// <summary>
    ///
    /// </summary>
    public class ChangeUserAccountTypeDto
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        public string NewType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        public string UserId { get; set; }
    }
}
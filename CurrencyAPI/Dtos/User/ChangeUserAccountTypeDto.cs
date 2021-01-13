using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.User
{
    public class ChangeUserAccountTypeDto
    {
        [Required]
        public string NewType { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
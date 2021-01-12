using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos.User
{
    public class ChangeUserAccountTypeDto
    {
        public string NewType { get; set; }
        public string UserId { get; set; }
    }
}
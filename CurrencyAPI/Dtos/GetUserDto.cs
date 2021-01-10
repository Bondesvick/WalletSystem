using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletSystemAPI.Dtos
{
    public class GetUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MainCurrencyId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Type { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }
    }
}
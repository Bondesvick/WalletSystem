using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string id);

        bool RegisterUser(User user);

        List<User> GetAllUsers();

        bool LoginUser(string id);

        bool DeleteUser(string id);
    }
}
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

        Task<bool> RegisterUser(User user, string password);

        List<User> GetAllUsers();

        Task<string> LoginUser(string id);

        Task<bool> DeleteUser(string id);

        Task<IList<string>> GetUserRoles(User user);
    }
}
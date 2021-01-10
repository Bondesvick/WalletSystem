using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface IUserRepository
    {
        public GetUserDto MapUser(string id);

        User GetUserById(string id);

        Task<User> GetUserByEmail(string email);

        Task<bool> RegisterUser(User user, string password);

        List<User> GetAllUsers();

        Task<string> LoginUser(UserToLoginDto userToLoginDto);

        Task<bool> DeleteUser(string id);

        Task<IList<string>> GetUserRoles(User user);
    }
}
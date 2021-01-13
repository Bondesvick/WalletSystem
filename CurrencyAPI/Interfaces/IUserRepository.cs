using System.Collections.Generic;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GetUserDto MapUser(string id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        GetUserDto GetMyDetails();

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User GetUserById(string id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByEmail(string email);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> RegisterUser(User user, string password);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        void AddUserToRole(User user, string role);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<User> GetAllUsers();

        /// <summary>
        ///
        /// </summary>
        /// <param name="userToLoginDto"></param>
        /// <returns></returns>
        Task<string> LoginUser(UserToLoginDto userToLoginDto);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IList<string>> GetUserRoles(User user);

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <param name="oldRole"></param>
        /// <param name="newRole"></param>
        /// <returns></returns>
        Task<bool> ChangeUserRole(User user, string oldRole, string newRole);
    }
}
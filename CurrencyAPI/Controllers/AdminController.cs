using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Dtos.Funding;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;

namespace WalletSystemAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFundRepository _fundRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundRepository"></param>
        /// <param name="walletRepository"></param>
        /// <param name="userRepository"></param>
        public AdminController(IFundRepository fundRepository, IWalletRepository walletRepository, IUserRepository userRepository)
        {
            _fundRepository = fundRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Allows admins to get all User details
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var usersToReturn = users.Select(u => _userRepository.MapUser(u.Id)).ToList();
            return Ok(ResponseMessage.Message("List of all users", null, usersToReturn));
        }

        /// <summary>
        /// Allows only admins to get wallet infos
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllWallets")]
        public IActionResult GetAllWallets()
        {
            var allWallets = _walletRepository.GetAllWallets();

            var wallets = allWallets.Select(w => new GetWalletDto()
            {
                WalletId = w.Id,
                CurrencyCode = w.Currency.Code,
                Balance = w.Balance,
                OwnerId = w.OwnerId,
                IsMain = w.IsMain
            }).ToList();

            return Ok(ResponseMessage.Message("List of all wallets", null, wallets));
        }

        /// <summary>
        /// Allows an admin to get all noob funds yet to be approved
        /// </summary>
        /// <returns>Admin Route</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetUnApprovedFundings")]
        public IActionResult GetUnApprovedFundings()
        {
            var fundings = _fundRepository.GetUnApprovedFundings();

            var toFund = fundings.Select(f => new GetFundingDto()
            {
                Amount = f.Amount,
                CurrencyCode = f.Currency.Code,
                FundingId = f.Id,
                WalletId = f.DestinationId,
            }).ToList();

            return Ok(ResponseMessage.Message("List of all Nood fundings yet to be approved", null, toFund));
        }

        /// <summary>
        /// Admin can change the main currency of a User
        /// </summary>
        /// <param name="changeMainCurrencyDto"></param>
        /// <returns>Response</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeUserMainCurrency")]
        public async Task<IActionResult> ChangeUserMainCurrency(ChangeMainCurrencyDto changeMainCurrencyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var old = _walletRepository.GetWalletById(changeMainCurrencyDto.OldMainCurrencyWalletId);
            var @new = _walletRepository.GetWalletById(changeMainCurrencyDto.NewMainCurrencyWalletId);

            if (old == null || @new == null)
                return BadRequest(ResponseMessage.Message("one of the ids entered is incorrect", "wallet to found", changeMainCurrencyDto));

            if (old.OwnerId != @new.OwnerId)
                return BadRequest(ResponseMessage.Message("Wallets user do not match", "Wallets does not belong to the same user", changeMainCurrencyDto));

            var changed = await _walletRepository.ChangeMainCurrency(old, @new);
            if (!changed)
                return BadRequest(ResponseMessage.Message("Unable to change main currency", "error encountered while trying to save main currency", changeMainCurrencyDto));

            return Ok(ResponseMessage.Message("Main currency changed successfully", null, changeMainCurrencyDto));
        }

        /// <summary>
        /// Admin can Promote or demote an account type
        /// </summary>
        /// <param name="changeUserAccountTypeDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("ChangeUserAccountType")]
        public async Task<IActionResult> ChangeUserAccountType(ChangeUserAccountTypeDto changeUserAccountTypeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var user = _userRepository.GetUserById(changeUserAccountTypeDto.UserId);
            if (user == null)
                return BadRequest(ResponseMessage.Message("Invalid user Id", "user with the id was not found", changeUserAccountTypeDto));

            if (changeUserAccountTypeDto.NewType != "Noob" && changeUserAccountTypeDto.NewType != "Elite")
                return BadRequest(ResponseMessage.Message("Invalid User Role", "User role can only be Noob or Elite", changeUserAccountTypeDto));

            var roles = await _userRepository.GetUserRoles(user);
            var oldRole = roles.FirstOrDefault();

            if (oldRole == null)
            {
                _userRepository.AddUserToRole(user, changeUserAccountTypeDto.NewType);
            }
            else
            {
                await _userRepository.ChangeUserRole(user, oldRole, changeUserAccountTypeDto.NewType);
            }

            return Ok(ResponseMessage.Message("Account type changed successfully", null, changeUserAccountTypeDto));
        }

        /// <summary>
        /// Admin can approve the funding of a Noob account holder
        /// </summary>
        /// <param name="approveFundingDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("ApproveFunding")]
        public async Task<IActionResult> ApproveFunding(ApproveFundingDto approveFundingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState));

            var funding = _fundRepository.GetFundingById(approveFundingDto.FundingId);
            if (funding == null)
                return BadRequest(ResponseMessage.Message("Invalid funding Id", "funding with the id was not found", approveFundingDto));

            var wallet = _walletRepository.GetWalletById(funding.DestinationId);

            if (wallet == null)
                return BadRequest(ResponseMessage.Message("Invalid wallet Id", "wallet with the id was not found", approveFundingDto));

            var funded = await _walletRepository.FundNoobWallet(funding);

            if (!funded)
                return BadRequest(ResponseMessage.Message("Unable to fund account", "and error was encountered while trying to fund this account", approveFundingDto));

            await _fundRepository.DeleteFunding(approveFundingDto.FundingId);

            return Ok(ResponseMessage.Message("Noob account funded", null, approveFundingDto));
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Helpers;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///
        /// </summary>
        /// <param name="walletRepository"></param>
        /// <param name="currencyRepository"></param>
        /// <param name="userRepository"></param>
        public WalletController(IWalletRepository walletRepository, ICurrencyRepository currencyRepository, IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Allows logged in Elite or Noob account holders to create a wallet
        /// </summary>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Elite, Noob")]
        [HttpPost("CreateWallet")]
        public async Task<IActionResult> CreateWallet(CreateWalletDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, walletDto));

            var loggedInUserId = _walletRepository.GetUserId();

            var userWallets = _walletRepository.GetWalletsByUserId(loggedInUserId);

            var user = _userRepository.GetUserById(loggedInUserId);
            var userRoles = await _userRepository.GetUserRoles(user);

            if (userRoles.Contains("Noob") && userWallets.Count > 0)
                return BadRequest(ResponseMessage.Message("Already has a wallet",
                    "your account type is only allowed to have one wallet", walletDto));

            var wallet = new Wallet()
            {
                Balance = 0,
                OwnerId = loggedInUserId,
                CurrencyId = walletDto.CurrencyId,
                IsMain = false
            };

            var created = _walletRepository.AddWallet(wallet);

            if (!created)
                return BadRequest(ResponseMessage.Message("Unable to create wallet",
                    "error encountered while creating wallet", walletDto));

            return Ok(ResponseMessage.Message("Wallet successfully created", null, walletDto));
        }

        /// <summary>
        /// Allows logged in Elite or Noob account holders to delete their wallet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Elite, Noob")]
        [HttpDelete("DeleteWallet/{id}")]
        public async Task<IActionResult> DeleteWallet(int id)
        {
            var deleted = await _walletRepository.DeleteWallet(id);

            if (!deleted)
                return BadRequest(ResponseMessage.Message("Unable to delete wallet", "error encountered while deleting the wallet", id));

            return Ok(ResponseMessage.Message("Wallet successfully deleted", null, id));
        }

        /// <summary>
        /// Allows logged in Elite or Noob account holders to update their wallet
        /// </summary>
        /// <param name="walletDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Noob")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateWallet(UpdateWalletDto walletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, walletDto));

            var wallet = _walletRepository.GetWalletById(walletDto.WalletId);

            if (wallet == null)
                return BadRequest(ResponseMessage.Message("Unable to update wallet", "invalid wallet id", walletDto));

            var loggedInUserId = _walletRepository.GetUserId();

            if (wallet.OwnerId != loggedInUserId)
                return BadRequest(ResponseMessage.Message("Invalid", "This wallet is not owned by you", walletDto));

            var updated = await _walletRepository.UpdateWallet(walletDto);

            if (!updated)
                return BadRequest(ResponseMessage.Message("Unable to update wallet", "error encountered while updating the wallet", walletDto));

            return Ok(ResponseMessage.Message("Wallet successfully updated", null, walletDto));
        }

        /// <summary>
        /// Allows logged-in Admin account holders to get a wallet by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetWalletDetail/{id}")]
        public IActionResult GetWallet(int id)
        {
            var wallet = _walletRepository.GetWalletById(id);

            if (wallet == null)
                return BadRequest(ResponseMessage.Message("Wallet not found", "invalid wallet id", id));

            var theWallet = new GetWalletDto()
            {
                WalletId = wallet.Id,
                CurrencyCode = wallet.Currency.Code,
                Balance = wallet.Balance,
                OwnerId = wallet.OwnerId,
                IsMain = wallet.IsMain
            };

            return Ok(ResponseMessage.Message("Successful!", null, theWallet));
        }

        /// <summary>
        /// Allows a user to fund a wallet
        /// </summary>
        /// <param name="fundingDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("FundWallet")]
        public async Task<IActionResult> FundWallet(FundingDto fundingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, fundingDto));

            var currencyExist = _currencyRepository.CurrencyExist(fundingDto.CurrencyId);

            if (!currencyExist)
                return NotFound(ResponseMessage.Message("Currency Not found", "currency id provided is invalid", fundingDto));

            var walletExist = _walletRepository.CheckWallet(fundingDto.WalletId);

            if (!walletExist)
                return NotFound(ResponseMessage.Message("Wallet Not found", "wallet id provided is invalid", fundingDto));

            var user = _userRepository.GetUserById(fundingDto.WalletOwnerId);

            if (user == null)
                return NotFound(ResponseMessage.Message("User Not found", "user id provided is invalid", fundingDto));

            var userRoles = await _userRepository.GetUserRoles(user);

            if (userRoles.Contains("Noob"))
            {
                var noobWalletFunded = await _walletRepository.FundNoobWallet(fundingDto);

                if (!noobWalletFunded)
                    return BadRequest(ResponseMessage.Message("Unable to fund wallet", "An error was encountered while trying to fund the wallet", fundingDto));

                return Ok(ResponseMessage.Message("Successfully funded, waiting approval from an Admin", null, fundingDto));
            }

            var walletFunded = await _walletRepository.FundWallet(fundingDto);

            if (!walletFunded)
                return BadRequest(ResponseMessage.Message("Unable to fund wallet", "An error was encountered while trying to fund the wallet", fundingDto));

            return Ok(ResponseMessage.Message("Wallet successfully funded", null, fundingDto));
        }

        /// <summary>
        /// Allows only Elite or Noob account holder to debit their wallets
        /// </summary>
        /// <param name="withdrawalDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Elite, Noob")]
        [HttpPost("WithdrawFromWallet")]
        public async Task<IActionResult> WithdrawFromWallet(WithdrawalDto withdrawalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseMessage.Message("Invalid Model", ModelState, withdrawalDto));

            var currencyExist = _currencyRepository.CurrencyExist(withdrawalDto.CurrencyId);

            if (!currencyExist)
                return NotFound(ResponseMessage.Message("Currency Not found", "currency id provided is invalid", withdrawalDto));

            var walletExist = _walletRepository.CheckWallet(withdrawalDto.WalletId);

            if (!walletExist)
                return NotFound(ResponseMessage.Message("Wallet Not found", "wallet id provided is invalid", withdrawalDto));

            var loggedInUserId = _walletRepository.GetUserId();
            var userWallets = _walletRepository.GetWalletsByUserId(loggedInUserId);

            if (userWallets.All(w => w.Id != withdrawalDto.WalletId))
                return BadRequest(ResponseMessage.Message("Unable to withdraw from this wallet", "This wallet is not owned by you", withdrawalDto));

            var walletDebited = await _walletRepository.WithdrawFromWallet(withdrawalDto);

            if (!walletDebited)
                return BadRequest(ResponseMessage.Message("Unable to withdraw from wallet", "An error was encountered while trying to withdraw from the wallet", withdrawalDto));

            return Ok(ResponseMessage.Message("You have successfully debited the walled", null, withdrawalDto));
        }

        /// <summary>
        /// Allows Noob or Elite account holder to get all their wallet(s)
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Elite, Noob")]
        [HttpGet("GetAllMyWallets")]
        public IActionResult GetAllMyWallets()
        {
            var myWallets = _walletRepository.GetAllMyWallets();

            var wallets = myWallets.Select(w => new GetWalletDto()
            {
                WalletId = w.Id,
                CurrencyCode = w.Currency.Code,
                Balance = w.Balance,
                OwnerId = w.OwnerId,
                IsMain = w.IsMain
            }).ToList();

            return Ok(ResponseMessage.Message("List of all wallets you own", null, wallets));
        }

        /// <summary>
        /// Allows only Admin to get a particular wallet by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetWalletsByUserId/{id}")]
        public IActionResult GetWalletsByUserId(string id)
        {
            var myWallets = _walletRepository.GetWalletsByUserId(id);

            var wallets = myWallets.Select(w => new GetWalletDto()
            {
                WalletId = w.Id,
                CurrencyCode = w.Currency.Code,
                Balance = w.Balance,
                OwnerId = w.OwnerId,
                IsMain = w.IsMain
            }).ToList();

            return Ok(ResponseMessage.Message("List of all wallets owned by the user", null, wallets));
        }
    }
}
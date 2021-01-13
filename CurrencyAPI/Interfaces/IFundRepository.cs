using System.Collections.Generic;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    /// <summary>
    ///
    /// </summary>
    public interface IFundRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Funding GetFundingById(int id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="fundingDto"></param>
        /// <returns></returns>
        Task<bool> CreateFunding(FundingDto fundingDto);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFunding(int id);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Funding> GetAllFundings();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Funding> GetUnApprovedFundings();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Funding> GetApprovedFundings();
    }
}
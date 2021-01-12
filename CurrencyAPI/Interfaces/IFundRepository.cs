using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    public interface IFundRepository
    {
        Funding GetFundingById(int id);

        Task<bool> CreateFunding(FundingDto fundingDto);

        Task<bool> DeleteFunding(int id);

        List<Funding> GetAllFundings();

        List<Funding> GetUnApprovedFundings();

        List<Funding> GetApprovedFundings();
    }
}
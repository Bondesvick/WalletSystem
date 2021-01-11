using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Dtos.Wallet;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Interfaces
{
    internal interface IFundRepository
    {
        Funding GetFundingById(int id);

        bool CreateFunding(FundingDto fundingDto);

        bool DeleteFunding(int id);

        List<Funding> GetAllFundings();
    }
}
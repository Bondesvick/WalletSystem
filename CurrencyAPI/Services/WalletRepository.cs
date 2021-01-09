using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletSystemAPI.Data;
using WalletSystemAPI.Interfaces;
using WalletSystemAPI.Models;

namespace WalletSystemAPI.Services
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DataContext _context;

        public WalletRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddWallet(Wallet wallet)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWallet(int id)
        {
            throw new NotImplementedException();
        }

        public Wallet GetWalletById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Wallet> GetAllWallets()
        {
            throw new NotImplementedException();
        }
    }
}
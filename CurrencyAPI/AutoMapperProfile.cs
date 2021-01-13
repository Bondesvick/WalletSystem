using AutoMapper;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Models;

namespace WalletSystemAPI
{
    /// <summary>
    ///
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        ///
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Currency, GetCurrencyDto>();
        }
    }
}
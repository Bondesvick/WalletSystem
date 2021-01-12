using AutoMapper;
using WalletSystemAPI.Dtos;
using WalletSystemAPI.Dtos.Currency;
using WalletSystemAPI.Dtos.User;
using WalletSystemAPI.Models;

namespace WalletSystemAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Currency, GetCurrencyDto>();
        }
    }
}
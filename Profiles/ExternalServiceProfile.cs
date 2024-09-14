using AutoMapper;
using GkoTradeService.Dtos;
using GkoTradeService.Models;

namespace GkoTradeService.Profiles
{
    public class ExternalServiceProfile : Profile
    {
        public ExternalServiceProfile()
        {
            // Source -> Target
            CreateMap<CryptoPrice, CryptoPriceGetDto>();
            CreateMap<CryptoPriceAddDto, CryptoPrice>();
        }
    }
}
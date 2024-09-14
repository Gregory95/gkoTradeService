using GkoTradeService.Domain;
using GkoTradeService.Enumerations;
using GkoTradeService.Interfaces;

namespace GkoTradeService.Application.Factories
{
    public class BitcoinPriceFactory
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public BitcoinPriceFactory(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public IPriceProviders GetSourceConfiguration(PriceSourcesEnum source)
        {
            switch (source)
            {
                case PriceSourcesEnum.BitStamp:
                    return new BitStampPrice(_httpClient, _configuration);
                case PriceSourcesEnum.BitFinex:
                    return new BitFinexPrice(_httpClient, _configuration);
                default:
                    return null;
            }
        }
    }
}


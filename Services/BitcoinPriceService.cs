using GkoTradeService.Application.Factories;
using GkoTradeService.Dtos;
using GkoTradeService.Enumerations;
using GkoTradeService.Interfaces;

namespace GkoTradeService.Services
{
    public class BitcoinPriceService : IBitcoinPriceService
    {
        private readonly BitcoinPriceFactory _bitcoinPriceFactory;

        public BitcoinPriceService(BitcoinPriceFactory bitcoinPriceFactory)
        {
            _bitcoinPriceFactory = bitcoinPriceFactory;
        }

        public async Task<float> CalculateBitcoinPrice(DateTime requestDate)
        {
            var sources = Enum.GetValues(typeof(PriceSourcesEnum));

            float aggregatedPrice = 0;

            foreach (PriceSourcesEnum priceSource in sources)
            {
                // Get source implementation
                var sourceResult = _bitcoinPriceFactory.GetSourceConfiguration(priceSource);
                // Retrieve the bitcoin price from source
                var bitcoinPrice = await sourceResult.GetBitcoinPrice(requestDate);
                // assign bitcoin value into aggregatedPrice variable
                aggregatedPrice += bitcoinPrice.Price;
            }

            return aggregatedPrice / sources.Length;
        }
    }
}
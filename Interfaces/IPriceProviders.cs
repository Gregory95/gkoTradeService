
using GkoTradeService.Dtos;

namespace GkoTradeService.Interfaces
{
    public interface IPriceProviders
    {
        Task<PriceProvidersResultDto> GetBitcoinPrice(DateTime requestDate);
    }
}
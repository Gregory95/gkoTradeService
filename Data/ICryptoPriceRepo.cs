using GkoTradeService.Dtos;
using GkoTradeService.Enumerations;
using GkoTradeService.Models;

namespace GkoTradeService.Data
{
    public interface ICryptoPriceRepo
    {
        IEnumerable<CryptoPriceGetDto> GetCryptoPrices();
        IEnumerable<CryptoPriceGetDto> GetCryptoPricesUserTimeRange(DateTime start, DateTime end);
        CryptoPriceGetDto GetCryptoPriceById(int id);
        Task<CryptoPrice> GetLatestCryptoPrice(CryptosEnum crypto, DateTime requestedTime);
        void AddCryptoPrice(CryptoPriceAddDto model);
    }
}
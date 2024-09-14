namespace GkoTradeService.Interfaces
{
    public interface IBitcoinPriceService
    {
        Task<float> CalculateBitcoinPrice(DateTime requestDate);
    }
}
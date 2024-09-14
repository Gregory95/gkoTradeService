namespace GkoTradeService.Dtos
{
    public class BitStampResponseDto
    {
        public string Pair { get; set; }
        public ICollection<Ohlc> Ohlc { get; set; }
    }

    public class BitStampApiResponse
    {
        public BitStampResponseDto Data { get; set; }
    }
}
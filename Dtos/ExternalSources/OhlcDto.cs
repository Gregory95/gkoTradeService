namespace GkoTradeService.Dtos
{
    public class Ohlc
    {
        public long Timestamp { get; set; }
        public int Open { get; set; }
        public int Close { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public float Volume { get; set; }
    }
}

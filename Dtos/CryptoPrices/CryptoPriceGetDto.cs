using GkoTradeService.Enumerations;

namespace GkoTradeService.Dtos
{
    public class CryptoPriceGetDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public CryptosEnum Crypto { get; set; }
        public string CryptoName { get; set; }
        public float Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
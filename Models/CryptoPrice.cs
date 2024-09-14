using System.ComponentModel.DataAnnotations;
using GkoTradeService.Enumerations;

namespace GkoTradeService.Models
{
    public class CryptoPrice
    {
        [Key]
        public int Id { get; set; }
        public required DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public required CryptosEnum Crypto { get; set; }
        public required float Price { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}
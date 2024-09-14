using System.ComponentModel.DataAnnotations;
using GkoTradeService.Enumerations;
using Microsoft.EntityFrameworkCore.Storage;

namespace GkoTradeService.Dtos
{
    public class CryptoPriceAddDto
    {
        public required CryptosEnum Crypto { get; set; }
        public required float Price { get; set; }
        public required DateTime Timestamp { get; set; }
    }
}
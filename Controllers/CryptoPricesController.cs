using Microsoft.AspNetCore.Mvc;
using GkoTradeService.Data;
using GkoTradeService.Dtos;
using GkoTradeService.Application.Factories;
using GkoTradeService.Enumerations;
using GkoTradeService.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoPricesController : ControllerBase
    {
        private readonly ICryptoPriceRepo _cryptoPricesRepo;
        private readonly IBitcoinPriceService _bitcoinPriceService;

        public CryptoPricesController(ICryptoPriceRepo cryptoPricesRepo, IBitcoinPriceService bitcoinPriceService)
        {
            _cryptoPricesRepo = cryptoPricesRepo;
            _bitcoinPriceService = bitcoinPriceService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CryptoPriceGetDto>> GetCryptoPrices([FromQuery] string start, [FromQuery] string end)
        {
            if (string.IsNullOrEmpty(start) && string.IsNullOrEmpty(end))
            {
                return Ok(_cryptoPricesRepo.GetCryptoPrices());
            }
            else if (DateTime.TryParse(start, out DateTime startDate) && DateTime.TryParse(end, out DateTime endDate))
            {
                return Ok(_cryptoPricesRepo.GetCryptoPricesUserTimeRange(startDate, endDate));
            }
            else
            {
                return BadRequest(new { Title = "Invalid Parameters", Description = "Start date or end date are not in the correct format." });
            }
        }

        [HttpGet("aggregated-bitcoin-price")]
        public async Task<ActionResult<PriceProvidersResultDto>> GetAggregatedBitcoinPrice([FromQuery] string timestamp)
        {
            // Validate and parse the timestamp
            if (!DateTime.TryParse(timestamp, out DateTime requestTime))
            {
                return BadRequest(new { message = "Invalid timestamp format. Use ISO 8601 format." });
            }

            requestTime = new DateTime(requestTime.Year, requestTime.Month, requestTime.Day, requestTime.Hour, 0, 0);

            var bitcoinSpecificPrice = await _cryptoPricesRepo.GetLatestCryptoPrice(CryptosEnum.Bitcoin, requestTime);

            // If existing price found then return it
            if (bitcoinSpecificPrice != null)
            {
                return Ok(new PriceProvidersResultDto
                {
                    Price = bitcoinSpecificPrice.Price
                });
            }

            var aggregatedBitcoinPrice = await _bitcoinPriceService.CalculateBitcoinPrice(requestTime);

            // Save new aggregated price
            _cryptoPricesRepo.AddCryptoPrice(new CryptoPriceAddDto
            {
                Crypto = CryptosEnum.Bitcoin,
                Price = aggregatedBitcoinPrice,
                Timestamp = requestTime
            });

            return Ok(new PriceProvidersResultDto { Price = aggregatedBitcoinPrice });
        }
    }
}
using System.Text.Json;
using System.Web;
using GkoTradeService.Interfaces;
using GkoTradeService.Dtos;

namespace GkoTradeService.Domain
{
    public class BitFinexPrice : IPriceProviders
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BitFinexPrice(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PriceProvidersResultDto> GetBitcoinPrice(DateTime requestDate)
        {
            var builder = new UriBuilder(_configuration["ExternalClients:BitfinexURL"]);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["start"] = new DateTimeOffset(requestDate).ToUnixTimeSeconds().ToString();
            query["end"] = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);

            PriceProvidersResultDto result = new PriceProvidersResultDto();

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> BitFinex Service is OK!");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var rawData = JsonSerializer.Deserialize<JsonElement[]>(jsonResponse);

                if (rawData == null || rawData.Length != 6)
                {
                    throw new InvalidOperationException("Invalid data structure received from API");
                }

                // Based on the provider`s API documentation this is the correct order of the response
                // --> https://docs.bitfinex.com/reference/rest-public-candles
                // Manually map the array elements to the DTO
                var ohlcDto = new Ohlc
                {
                    Timestamp = rawData[0].GetInt64(),
                    Open = rawData[1].GetInt32(),
                    Close = rawData[2].GetInt32(),
                    High = rawData[3].GetInt32(),
                    Low = rawData[4].GetInt32(),
                    Volume = rawData[5].GetSingle()
                };

                result.Price = ohlcDto.Close;

                return result;
            }
            else
            {
                Console.WriteLine("--> BitFinex Service is NOT OK!");
                return result;
            }
        }
    }
}
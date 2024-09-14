using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using GkoTradeService.Interfaces;
using GkoTradeService.Dtos;

namespace GkoTradeService.Domain
{
    public class BitStampPrice : IPriceProviders
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BitStampPrice(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<PriceProvidersResultDto> GetBitcoinPrice(DateTime requestDate)
        {
            var builder = new UriBuilder(_configuration["ExternalClients:BitstampURL"]);
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["step"] = "3600";
            query["limit"] = "1";
            query["start"] = new DateTimeOffset(requestDate).ToUnixTimeSeconds().ToString();

            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> BitStamp Service is OK!");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                // Handle json string number values
                var options = new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
                    PropertyNameCaseInsensitive = true
                };

                var apiResult = JsonSerializer.Deserialize<BitStampApiResponse>(jsonResponse, options);

                PriceProvidersResultDto result = new PriceProvidersResultDto();

                result.Price = apiResult.Data.Ohlc.FirstOrDefault().Close;

                return result;
            }
            else
            {
                Console.WriteLine("--> BitStamp Service is NOT OK!");
                return null;
            }
        }
    }
}
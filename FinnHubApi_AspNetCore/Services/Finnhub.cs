using System.Text.Json;

namespace FinnHubApi_AspNetCore.Services
{
    public class Finnhub : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public Finnhub(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            using(HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMesssage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol??_configuration.GetValue<string>("TradingOptions:DefaultStockSymbol")}&token={_configuration.GetValue<string>("FinhubApi:ApiKey")}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMesssage);

                Stream stream = responseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                return JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            }
        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}

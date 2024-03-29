﻿using System.Text.Json;
using static System.Net.WebRequestMethods;

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
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol)
        {
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMesssage = new HttpRequestMessage()
                {   

                    //RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol?? _configuration.GetValue<string>("TradingOptions:DefaultStockSymbol")}&token={_configuration.GetValue<string>("FinhubApi:ApiKey")}"),
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol??_configuration.GetValue<string>("TradingOptions:DefaultStockSymbol")}&token={_configuration.GetValue<string>("FinhubApi:ApiKey")}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await client.SendAsync(httpRequestMesssage);

                Stream stream = responseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                if (response.Contains("error"))
                    return null;

                return JsonSerializer.Deserialize<Dictionary<string, object>>(response);
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol)
        {
            using(HttpClient client =  _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol ?? _configuration.GetValue<string>("TradingOptions:DefaultStockSymbol")}&token={_configuration.GetValue<string>("FinhubApi:ApiKey")}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);
                
                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd() ;

                if (response.Contains("error"))
                    return null;

                return JsonSerializer.Deserialize<Dictionary<string, object>?>(response);
            }
        }
    }
}

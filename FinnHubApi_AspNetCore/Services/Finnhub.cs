namespace FinnHubApi_AspNetCore.Services
{
    public class Finnhub : IFinnhubService
    {
        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            throw new NotImplementedException();
        }
    }
}

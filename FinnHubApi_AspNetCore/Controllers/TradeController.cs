using FinnHubApi_AspNetCore.Models;
using FinnHubApi_AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FinnHubApi_AspNetCore.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _iFin;
        private readonly IConfiguration _configuration;
        public TradeController(IFinnhubService iFin, IConfiguration configuration)
        {
            _iFin = iFin;
            _configuration = configuration;
        }

        [Route("/")]
        //[Route("/finhub/token/{symbol}")]
        public async Task<IActionResult> Index(string? symbol = null)
        {
            var companyProfile = await _iFin.GetCompanyProfile(symbol);
            var stockPrice = await _iFin.GetStockPriceQuote(symbol);

            var stockTrade = new StockTrade()
            {
                StockSymbol = Convert.ToString(companyProfile!["ticker"]??null),
                StockName = Convert.ToString(companyProfile["name"]??null),
                Price = Convert.ToDouble(Convert.ToString(stockPrice!["c"] ?? null)),
                WebUrl = Convert.ToString(companyProfile["weburl"] ?? null),
            };

            ViewBag.Token = _configuration.GetValue<string>("FinhubApi:ApiKey");
            return View("Index", stockTrade);
        }

        [Route("/finhub/price/{symbol}")]
        public async Task<IActionResult> GetTokenPrice(string? symbol = null)
        {
            return Json(await _iFin.GetStockPriceQuote(symbol));
        }
    }
}

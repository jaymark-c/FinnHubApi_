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
        public async Task<IActionResult> Index(string? symbol = null)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                var companyProfile = await _iFin.GetCompanyProfile(symbol);
                var stockPrice = await _iFin.GetStockPriceQuote(symbol);


                var stockTrade = new StockTrade()
                {
                    StockSymbol = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["ticker"] ?? null),
                    StockName = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["name"] ?? null),
                    Price = Convert.ToDouble(Convert.ToString(stockPrice!["c"] ?? null)),
                    WebUrl = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["weburl"] ?? null),
                };

                ViewBag.Token = _configuration.GetValue<string>("FinhubApi:ApiKey");
                return View("Index", stockTrade);
            }
            else
            {
                return View("Index");
            }
        }

        [Route("/finhub/token/{symbol}")]
        public async Task<IActionResult> TokenSearch(string? symbol = null)
        {
            var companyProfile = await _iFin.GetCompanyProfile(symbol);
            var stockPrice = await _iFin.GetStockPriceQuote(symbol);


            var stockTrade = new StockTrade()
            {
                StockSymbol = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["ticker"] ?? null),
                StockName = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["name"] ?? null),
                Price = Convert.ToDouble(Convert.ToString(stockPrice!["c"] ?? null)),
                WebUrl = companyProfile.Count == 0 ? null : Convert.ToString(companyProfile["weburl"] ?? null),
            };

            ViewBag.Token = _configuration.GetValue<string>("FinhubApi:ApiKey");
            return View("TokenSearch", stockTrade);
        }

        [Route("/finhub/price/{symbol}")]
        public async Task<IActionResult> GetTokenPrice(string? symbol = null)
        {
            return Json(await _iFin.GetStockPriceQuote(symbol));
        }
    }
}

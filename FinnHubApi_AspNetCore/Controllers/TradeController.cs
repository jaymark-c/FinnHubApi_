using FinnHubApi_AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinnHubApi_AspNetCore.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _iFin; 
        public TradeController(IFinnhubService iFin)
        {
            _iFin = iFin;
        }

        [Route("/")]
        [Route("/finhub/profile/{symbol}")]
        public async Task<IActionResult> Index(string? symbol = null)
        {
            return Json(await _iFin.GetCompanyProfile(symbol));
        }

        [Route("/finhub/price/{symbol}")]
        public async Task<IActionResult> GetTokenPrice(string? symbol = null)
        {
            return Json(await _iFin.GetStockPriceQuote(symbol));
        }
    }
}

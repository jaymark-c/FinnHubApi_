using FinnHubApi_AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinnHubApi_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFinnhubService _iFin; 
        public HomeController(IFinnhubService iFin)
        {
            _iFin = iFin;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            return Json(await _iFin.GetCompanyProfile(null));
        }
    }
}

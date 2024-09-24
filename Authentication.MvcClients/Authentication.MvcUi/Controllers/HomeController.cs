using Authentication.MvcUi.ApiServices.Interfaces;
using Authentication.MvcUi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.MvcUi.Controllers
{
    [ServiceFilter(typeof(SessionFilter))]
    public class HomeController : BaseController
    {
        public HomeController(IConfiguration configuration, IHttpApiService httpApiService) : base(configuration, httpApiService)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

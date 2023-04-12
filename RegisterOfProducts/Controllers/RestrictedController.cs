using Microsoft.AspNetCore.Mvc;
using RegisterOfProducts.Filters;

namespace RegisterOfProducts.Controllers
{
    [LoggedUser]
    public class RestrictedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

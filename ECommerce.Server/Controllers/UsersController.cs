using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Server.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

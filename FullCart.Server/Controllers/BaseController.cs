using Microsoft.AspNetCore.Mvc;

namespace FullCart.Server.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> logger;

        public BaseController(ILogger<BaseController> Logger)
        {
            logger = Logger;
        }

    }
}

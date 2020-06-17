using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dashboard.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : Controller
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }
        
        //[HttpGet]
    }
}
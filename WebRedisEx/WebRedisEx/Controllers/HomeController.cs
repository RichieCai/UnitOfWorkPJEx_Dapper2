using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Diagnostics;
using WebRedisEx.Models;

namespace WebRedisEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConnectionMultiplexer _redis;

        public HomeController(ILogger<HomeController> logger, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult User()
        {
            var db = _redis.GetDatabase();
            //db.StringSet(key, value);
            return Ok();
        }
        [HttpGet]
        public IActionResult Set(string key, string value)
        {
            var db = _redis.GetDatabase();
            db.StringSet(key, value);
            return Ok();
        }

        [HttpGet("get/{key}")]
        public IActionResult Get(string key)
        {
            var db = _redis.GetDatabase();
            var value = db.StringGet(key);
            return Ok(value);
        }
    }
}
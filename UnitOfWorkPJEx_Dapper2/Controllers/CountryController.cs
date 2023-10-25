using Microsoft.AspNetCore.Mvc;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        [HttpPost]
        public string Index()
        {
            return "";
        }
    }
}

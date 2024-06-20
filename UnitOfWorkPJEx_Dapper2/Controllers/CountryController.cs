using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;   
        public CountryController(ICountryService countryService)
        {
            _countryService=countryService;
        }

        [HttpGet("{CountryId}")]
        public async Task<IActionResult> Get(string CountryId)
        {
            var vResult= await  _countryService.Get(CountryId);
            if (vResult == null)
                return NotFound();

            return Ok(vResult);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vResult = await _countryService.GetAll();
            if (vResult == null|| vResult.Count()==0)
                return NotFound();

            return Ok(vResult);
        }
        [HttpGet("GetCountryCityAll")]
        public async Task<IActionResult> GetCountryCityAll()
        {
            var vResult = await _countryService.GetCountryCityAll();
            if (vResult == null || vResult.Count() == 0)
                return NotFound();

            return Ok(vResult);
        }

    }
}

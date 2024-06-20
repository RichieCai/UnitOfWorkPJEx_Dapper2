using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPJEx_DapperRepository.Context;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController2 : ControllerBase
    {
        private readonly IUserService2 _iUserService2;
        private ILogger<UserController> _logger;


        public UserController2(IUserService2 iUserService2, ILogger<UserController> logger)
        {
            _iUserService2 = iUserService2;
            _logger = logger;

        }

        [HttpGet("GetAll_unit")]
        public async Task<IActionResult> GetAll_unit()
        {
            _logger.LogInformation($"{this.GetType()},調用");
            // var UserList = await _dbcontext.Users.ToListAsync();
            var UserList = await _iUserService2.GetUserAll();
            if (UserList == null)
            {
                return NotFound();
            }
            return Ok(UserList);
        }

    }
}

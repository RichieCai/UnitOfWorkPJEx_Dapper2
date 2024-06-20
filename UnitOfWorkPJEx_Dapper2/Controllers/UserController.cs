using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPJEx_DapperRepository.Context;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iUserService;
        private ILogger<UserController> _logger;


        public UserController(IUserService iUserService,  ILogger<UserController> logger)
        {
            _iUserService = iUserService;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"{this.GetType()},調用");
            // var UserList = await _dbcontext.Users.ToListAsync();
            var UserList = await _iUserService.GetUserAll();
            if (UserList == null)
            {
                return NotFound();
            }
            return Ok(UserList);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> Get(int UserId)
        {
            _logger.LogInformation($"{this.GetType()},調用");
            var vUser = await _iUserService.GetById(UserId);
            if (vUser == null)
            {
                return NotFound();
            }
            return Ok(vUser);
        }

        [HttpGet("Query")]
        public async Task<IActionResult> Get([FromQuery] UserInput input)
        {
            _logger.LogInformation($"{this.GetType()},調用");
            var vUser = await _iUserService.Get(input);
            if (vUser == null)
            {
                return NotFound();
            }
            return Ok(vUser);
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("UserName,Age,Sex,CountryId,CityId")] User user)
        {
            _logger.LogInformation($"{this.GetType()},調用");
            var IsCreated = await _iUserService.AddAsync(user);
            if (IsCreated)
            {
                return Ok(IsCreated);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            _logger.LogInformation($"{this.GetType()},調用");
            var IsUpdate = await _iUserService.UpdateAsync(user);
            if (IsUpdate)
            {
                return Ok(IsUpdate);
            }
            return BadRequest();

        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            _logger.LogInformation($"{this.GetType()},調用");
            var IsDelete = await _iUserService.DeleteAsync(UserId);
            if (IsDelete)
            {
                return Ok(IsDelete);
            }
            return BadRequest();

        }
    }
}

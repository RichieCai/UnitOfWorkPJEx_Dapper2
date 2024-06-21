using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperService.Service;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserGenericController : ControllerBase
    {
        private readonly IUserGenericService _iuserService;
        private ILogger<UserController> _logger;
        private readonly IMapper _mapper;


        public UserGenericController(IUserGenericService iUserService, ILogger<UserController> logger, IMapper mapper)
        {
            _iuserService = iUserService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _iuserService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _iuserService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([Bind("UserName,Age,Sex,CountryId,CityId")] User user)
        {
           // var userData = _mapper.Map<UnitOfWorkPJEx_DapperRepository.Models.Data.User>(user);
            var userData = new UnitOfWorkPJEx_DapperRepository.Models.Data.User()
            {
                CityId = user.CityId,
                Sex = (byte)user.Sex,
                Age = user.Age,
                UserName = user.UserName,
                CountryId = user.CountryId,
            };
            await _iuserService.AddUserAsync(userData);
            return Ok("新增成功");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _iuserService.DeleteUserAsync(id);
            return Ok("刪除成功");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var userData = new UnitOfWorkPJEx_DapperRepository.Models.Data.User()
            {
                UserId = (int)user.UserId,
                CityId = user.CityId,
                Sex = (byte)user.Sex,
                Age = user.Age,
                UserName = user.UserName,
                CountryId = user.CountryId,
            };
            await _iuserService.UpdateUserAsync(userData);
            return Ok("更新成功");
        }

    }
}

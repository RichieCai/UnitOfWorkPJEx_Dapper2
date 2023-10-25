using Microsoft.AspNetCore.Mvc;
using UnitOfWorkPJEx_DapperRepository.Context;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_Dapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _iUserService;

        public UserController(IUserService iUserService)
        {
            _iUserService = iUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
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
            var vUser = await _iUserService.GetById(UserId);
            if (vUser == null)
            {
                return NotFound();
            }
            return Ok(vUser);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            var IsCreated = await _iUserService.AddUser(user);
            if (IsCreated)
            {
                return Ok(IsCreated);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var IsUpdate = await _iUserService.UpdateUser(user);
            if (IsUpdate)
            {
                return Ok(IsUpdate);
            }
            return BadRequest();

        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            var IsDelete = await _iUserService.DeleteUser(UserId);
            if (IsDelete)
            {
                return Ok(IsDelete);
            }
            return BadRequest();

        }
    }
}

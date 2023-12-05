using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitOfWorkPJEx_Dapper.Controllers;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository_Test.MockData;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_Dapper2_Test
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _userController;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _userController = new UserController(_userServiceMock.Object, _loggerMock.Object);

        }

        [Fact]
        public async Task GetAll_ReturnUserList_WhenUsersExist()
        {
            var userlist = MockData_ModelsData_User.GetUserAll();

            _userServiceMock.Setup(ser => ser.GetUserAll()).ReturnsAsync(userlist);

            var vResult = await _userController.GetAll();

            Assert.IsType<OkObjectResult>(vResult);
            var userResult = ((OkObjectResult)vResult).Value as IEnumerable<User>;
            Assert.NotNull(vResult);
            Assert.Equal(userlist.Count, userResult.Count());
            _userServiceMock.Verify(c => c.GetUserAll(), Times.Once);

        }
        [Fact]
        public async Task GetAll_ReturnNull_WhenUsersNoExist()
        {
            _userServiceMock.Setup(ser => ser.GetUserAll()).ReturnsAsync(() => null);

            var vResult = await _userController.GetAll();

            Assert.IsType<NotFoundResult>(vResult);
            _userServiceMock.Verify(c => c.GetUserAll(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOkUser_WhenUserExists()
        {
            int UserId = 1;
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());

            _userServiceMock.Setup(ser => ser.GetById(UserId)).ReturnsAsync(user);

            var vResult = await _userController.Get(UserId);

            Assert.IsType<OkObjectResult>(vResult);
            Assert.NotNull(vResult);
            var userResult = (User)((OkObjectResult)vResult).Value;
            Assert.Equal(userResult.UserId, user.UserId);
            _userServiceMock.Verify(c => c.GetById(UserId), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenUserDoesNotExist()
        {
            int UserId = -1;
            _userServiceMock.Setup(ser => ser.GetById(It.IsAny<int>())).ReturnsAsync((int UserId) => (User)null);
            // _userServiceMock.Setup(ser => ser.GetById(UserId)).ReturnsAsync(() => null);

            var vResult = await _userController.Get(UserId);

            Assert.IsType<NotFoundResult>(vResult);
            _userServiceMock.Verify(c => c.GetById(UserId), Times.Once);
        }


        [Fact]
        public async Task AddUser_ReturnTrue_WhenIsSuccess()
        {
            var user = MockData_ModelsData_User.AddUser();
            _userServiceMock.Setup(ser => ser.AddAsync(It.IsAny<User>())).ReturnsAsync(true);

            var vResult = await _userController.AddUser(user);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.AddAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ReturnTrue_WhenIsSuccess()
        {
            var user = MockData_ModelsData_User.UpdateUserResult();
            _userServiceMock.Setup(ser => ser.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

            var vResult = await _userController.UpdateUser(user);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.UpdateAsync(user), Times.Once);

        }

        [Fact]
        public async Task DeleteUser_ReturnTrue_WhenIsSuccess()
        {
            int UserId = 1;
            _userServiceMock.Setup(ser => ser.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            var vResult = await _userController.DeleteUser(UserId);

            Assert.IsType<OkObjectResult>(vResult);
            var okResult = (OkObjectResult)vResult;
            Assert.True((bool)okResult.Value);
            _userServiceMock.Verify(c => c.DeleteAsync(UserId), Times.Once);

        }
    }
}
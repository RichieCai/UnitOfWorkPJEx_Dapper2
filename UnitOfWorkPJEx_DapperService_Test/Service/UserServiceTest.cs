using MyCommon.Interface;
using Moq;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository_Test.MockData;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperService.Service;

namespace UnitOfWorkPJEx_DapperService_Test.Service
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository>  _userRepositoryMock;
        private readonly Mock<IMsDBConn> _msDBConnMock;
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock= new Mock<IUserRepository>();
            _msDBConnMock= new Mock<IMsDBConn>();
            _userService = new UserService(_msDBConnMock.Object, _userRepositoryMock.Object);
        }

        //Task<User> GetById(int UserId);
        [Theory]
        [InlineData(1)]
        public async void GetById_ReturnUser_IsSuccess(int UserId)
        {
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());
            _userRepositoryMock.Setup(m => m.GetById<User>(UserId.ToString())).ReturnsAsync(user);

            var actualUser = await _userService.GetById(UserId);

            Assert.NotNull(actualUser);
            Assert.Equal(user.UserId, actualUser.UserId);
            Assert.Equal(user.UserName, actualUser.UserName);
        }

        [Fact]
        public async void GetUserAll_ReturnUser_IsSuccess()
        {
            var userlist = MockData_ModelsData_User.GetUserAll();

            _userRepositoryMock.Setup(m => m.GetAll<User>()).ReturnsAsync(userlist);

            var actualUser=await _userService.GetUserAll();

            Assert.NotNull(actualUser);
            Assert.Equal(actualUser.Count(), userlist.Count());
        }

        [Fact]
        public async void AddAsync_ReturnUser_IsSuccess()
        {
            User userGet = null;
            User user = MockData_ModelsData_User.AddUser();

            _userRepositoryMock.Setup(m => m.GetById<User>(user.UserId.ToString())).ReturnsAsync(userGet);
            _userRepositoryMock.Setup(m => m.AddAsync(user)).ReturnsAsync(true);

            var actualResult = await _userService.AddAsync(user);

            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
            _msDBConnMock.Verify(conn => conn.Commit(), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_ReturnUser_IsSuccess()
        {
            var userResult = MockData_ModelsData_User.UpdateUserResult();

            _userRepositoryMock.Setup(m => m.GetById<User>(userResult.UserId.ToString())).ReturnsAsync(userResult);
            _userRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);

            var actualResult = await _userService.UpdateAsync(userResult);
            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
            _msDBConnMock.Verify(conn => conn.Commit(), Times.Once);

        }

        [Fact]
        public async void DeleteAsync_ReturnUser_IsSuccess()
        {
            string sDelete_UserId = "2";
            var user = MockData_ModelsData_User.GetUser(sDelete_UserId);
            _userRepositoryMock.Setup(m => m.GetById<User>(user.UserId.ToString())).ReturnsAsync(user);
            _userRepositoryMock.Setup(m => m.DeleteAsync(user)).ReturnsAsync(true);

            var actualResult = await _userService.DeleteAsync(int.Parse(sDelete_UserId));
            Assert.True(actualResult);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<User>()), Times.Once);
            _msDBConnMock.Verify(conn => conn.Commit(), Times.Once);

        }
    }
}

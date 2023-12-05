using Dapper;
using MyCommon.Interface;
using Moq;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Repository;
using UnitOfWorkPJEx_DapperRepository_Test.MockData;

namespace UnitOfWorkPJEx_DapperRepository_Test.Repository
{
    public class UsersRepositoryTest
    {
        private readonly Mock<IMsDBConn> _msDBConn;
        private readonly IUserRepository _userRepository;
        public UsersRepositoryTest()
        {
            _msDBConn = new Mock<IMsDBConn>();
            _userRepository = new UserRepository(_msDBConn.Object);

        }

        [Theory]
        [InlineData("1")]
        public async Task GetById_ReturnOneUser_WhenUserExists(string UserId)
        {
            //Arrange
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());
            _msDBConn.Setup(m => m.QuerySingleAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
                .ReturnsAsync(user);

            //Act
            var actualUser = await _userRepository.GetById<User>(UserId);

            //Assert
            Assert.NotNull(actualUser);
            Assert.Equal(user.UserId, actualUser.UserId);
            Assert.Equal(user.UserName, actualUser.UserName);
        }

        [Theory]
        [InlineData("3")]
        public async Task GetById_ReturnNull_WhenUserNotExists(string UserId)
        {
            //Arrange
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());
            _msDBConn.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
                .ReturnsAsync(new List<User> { user });
            //Act
            var actualUser = await _userRepository.GetById<User>(UserId);
            //Assert
            Assert.Null(actualUser);
        }

        [Fact]
        public async Task GetAll_ReturnAllUser_WhenUserHaveData()
        {
            //Arrange
            var userlist = MockData_ModelsData_User.GetUserAll();
            _msDBConn.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
                .ReturnsAsync(userlist);
            //Act
            var actualUserList = await _userRepository.GetAll<User>();

            //Assert
            Assert.NotNull(actualUserList);
            Assert.Equal(actualUserList.Count(), userlist.Count());
        }

        [Fact]
        public async Task AddAsync_ReturnTrue_WhenIsSuccess()
        {
            int iIsSunccess = 1;
            var user = MockData_ModelsData_User.AddUser();
            _msDBConn.Setup(m => m.AddAsync(user, It.IsAny<List<string>>()))
            .ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.AddAsync(user);

            Assert.True(actualResult);
        }
        [Fact]
        public async Task AddAsync_ReturnFalse_WhenIsFailed()
        {
            int iIsSunccess = 0;
            //var user = MockData_ModelsData_User.AddUser();
            User user = new User();
            _msDBConn.Setup(m => m.AddAsync(user, It.IsAny<List<string>>()))
            .ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.AddAsync(user);

            Assert.False(actualResult);
        }


        [Fact]
        public async Task UpdateAsync_ReturnTrue_WhenIsSuccess()
        {
            int iIsSunccess = 1;
            var user = MockData_ModelsData_User.UpdateUserResult();
            _msDBConn.Setup(m => m.UpdateAsync(It.IsAny<string[]>(), It.IsAny<User>(), It.IsAny<string[]>(), It.IsAny<User>()))
            .ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.UpdateAsync(user);

            Assert.True(actualResult);
        }

        [Fact]
        public async Task UpdateAsync_ReturnFalse_WhenIsFailed()
        {
            int iIsSunccess = 0;
            var userAll = MockData_ModelsData_User.GetUserAll();
            //var user = MockData_ModelsData_User.AddUser();
            User user = new User();
            _msDBConn.Setup(m => m.UpdateAsync(It.IsAny<string[]>(), It.IsAny<User>(), It.IsAny<string[]>(), It.IsAny<User>()))
            .ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.UpdateAsync(user);

            Assert.False(actualResult);
        }

        [Theory]
        [InlineData("2")]
        public async Task DeleteAsync_ReturnTrue_WhenIsSuccess(string UserId)
        {
            int iIsSunccess = 1;
            var user = MockData_ModelsData_User.GetUser(UserId.ToString());
            //_msDBConn.Setup(m => m.QueryAsync<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            //    .ReturnsAsync(new List<User> { user });
            _msDBConn.Setup(m => m.DeleteAsync(user)).ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.DeleteAsync(user);

            Assert.True(actualResult);
        }

        [Theory]
        [InlineData("3")]
        public async Task DeleteAsync_ReturnFlase_WhenIsFailed(string UserId)
        {
            int iIsSunccess = 0;
            User user = new User();

            _msDBConn.Setup(m => m.DeleteAsync(user)).ReturnsAsync(iIsSunccess);

            var actualResult = await _userRepository.DeleteAsync(user);

            Assert.False(actualResult);
        }
    }
}

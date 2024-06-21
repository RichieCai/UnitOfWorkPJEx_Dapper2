using Dapper;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork_Dapper _unitOfWork;
        public UserRepository(IUnitOfWork_Dapper unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> GetUserByIdAsync(int iUserId)
        {
            var sql = "SELECT * FROM [User] WHERE UserId = @UserId";
            return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<User>(sql, new { UserId = iUserId }, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var sql = "SELECT * FROM [User]";
            return await _unitOfWork.Connection.QueryAsync<User>(sql, null, _unitOfWork.Transaction);
        }

        public async Task<int> AddUserAsync(User user)
        {
            var sql = "INSERT INTO [User] (UserName, Age,Sex,CountryId,CityId) VALUES (@UserName, @Age,@Sex,@CountryId,@CityId)";
            var result= await _unitOfWork.Connection.ExecuteAsync(sql, user, _unitOfWork.Transaction);
            return result;
        }

        public async Task<int> AddUserAsyncReturn(User user)
        {
            var sql = @"
            INSERT INTO [User] (UserName, Age, Sex, CountryId, CityId) VALUES (@UserName, @Age, @Sex, @CountryId, @CityId);
            SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _unitOfWork.Connection.QuerySingleAsync<int>(sql, user, _unitOfWork.Transaction);
        }

        public async Task<int> DeleteUserAsync(int iUserId)
        {
            var sql = "DELETE FROM [User] WHERE UserId = @UserId";
            return await _unitOfWork.Connection.ExecuteAsync(sql, new { UserId = iUserId }, _unitOfWork.Transaction);
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            var sql = @"
            UPDATE [User]
            SET UserName = @UserName, Age = @Age, Sex = @Sex, CountryId = @CountryId, CityId = @CityId
            WHERE UserId = @UserId";
            return await _unitOfWork.Connection.ExecuteAsync(sql, user, _unitOfWork.Transaction);
        }
    }
}

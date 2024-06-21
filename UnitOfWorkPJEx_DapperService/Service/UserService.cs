using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;
using UnitOfWorkPJEx_DapperRepository.Repository;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork_Dapper _iunitOfWork_Dapper;

        public UserService(IUserRepository userRepository, IUnitOfWork_Dapper iunitOfWork_Dapper)
        {
            _iunitOfWork_Dapper = iunitOfWork_Dapper;
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
            _iunitOfWork_Dapper.Commit();
        }
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
                _iunitOfWork_Dapper.Commit();
            }
            catch (Exception ex)
            {
                _iunitOfWork_Dapper.Rollback();
                throw new Exception("刪除使用者時發生錯誤", ex);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await _userRepository.UpdateUserAsync(user);
                _iunitOfWork_Dapper.Commit();
            }
            catch (Exception ex)
            {
                _iunitOfWork_Dapper.Rollback();
                throw new Exception("更新使用者時發生錯誤", ex);
            }
        }
    }
}

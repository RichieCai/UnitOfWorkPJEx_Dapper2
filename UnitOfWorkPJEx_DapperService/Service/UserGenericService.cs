using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserGenericService : IUserGenericService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork_Dapper _iunitOfWork_Dapper;

        public UserGenericService(IGenericRepository<User> userRepository, IUnitOfWork_Dapper iunitOfWork_Dapper)
        {
            _iunitOfWork_Dapper = iunitOfWork_Dapper;
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id, "UserId");
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(user, "UserId");
            _iunitOfWork_Dapper.Commit();
        }
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id, "UserId");
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
                await _userRepository.UpdateAsync(user);
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

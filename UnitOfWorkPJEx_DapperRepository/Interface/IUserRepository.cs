using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUserRepository 
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<int> AddUserAsync(User user);
        Task<int> AddUserAsyncReturn(User user);
        Task<int> DeleteUserAsync(int iUserId);
        Task<int> UpdateUserAsync(User user);
    }
}

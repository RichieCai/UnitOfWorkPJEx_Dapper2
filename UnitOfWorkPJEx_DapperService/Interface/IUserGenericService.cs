using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserGenericService
    {

        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);

    }
}

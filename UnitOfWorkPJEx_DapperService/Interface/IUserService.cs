using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserService
    {
        Task<User> GetById(int UserId);

        Task<IEnumerable<User>> GetUserAll();

        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(int UserId);
    }
}

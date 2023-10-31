using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserService
    {
        Task<User?> GetById(int UserId);

        Task<IEnumerable<User>> GetUserAll();

        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int UserId);
    }
}

using Dapper;
using Generally;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUserRepository
    {
        Task<T?> GetById<T>(string UserId);
        Task<IEnumerable<User>> GetAll<User>();
        Task<bool> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        bool Update(User user);

        Task<bool> DeleteAsync(User user);
    }
}

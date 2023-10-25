using Dapper;
using Generally;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUserRepository
    {
        Task<T> GetById<T>(int UserId);
        Task<IEnumerable<User>> GetAll<User>();
        Task<bool> Add(User user);

        Task<bool> Update(User user);

        Task<bool> Delete(int iUserId);
    }
}

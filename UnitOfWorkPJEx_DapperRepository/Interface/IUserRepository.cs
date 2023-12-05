using Dapper;
using MyCommon;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUserRepository
    {
        Task<T?> GetById<T>(string UserId);
        Task<IEnumerable<User>> GetAll<User>();
        Task<ResultVM<UserVM>> Get<UserVM>(UserInput input);
        Task<bool> AddAsync(User user);

        Task<bool> UpdateAsync(User user);

        bool Update(User user);

        Task<bool> DeleteAsync(User user);
    }
}

using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserService
    {
        Task<User?> GetById(int UserId);

        Task<IEnumerable<User>> GetUserAll();

        Task<ResultVM<UserVM>> Get(UserInput input);

        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);

        Task<bool> DeleteAsync(int UserId);
    }
}

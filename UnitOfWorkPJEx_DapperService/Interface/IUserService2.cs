using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface IUserService2
    {

        Task<IEnumerable<User>> GetUserAll();

    }
}

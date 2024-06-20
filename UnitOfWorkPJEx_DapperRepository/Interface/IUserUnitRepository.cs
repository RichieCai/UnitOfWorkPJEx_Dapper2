using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUserUnitRepository 
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}

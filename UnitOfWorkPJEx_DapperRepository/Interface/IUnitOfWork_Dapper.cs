using System.Data;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUnitOfWork_Dapper 
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IUserRepository Users { get; }

        void Commit();
    }
}

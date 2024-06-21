using System.Data;

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IUnitOfWork_Dapper : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Commit();
        void Rollback();
    }
}

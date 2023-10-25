using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;


namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        internal IDbConnection _connection;
        internal IDbTransaction _transaction;

        protected GenericRepository(IUnitOfWork_Dapper unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }
        public TEntity GetById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserId", id);
            var sSqlCmd = "select * from [User] where UserId=@UserId";
            return _connection.QuerySingle<TEntity>(sSqlCmd, commandType: CommandType.Text);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var sSqlCmd = "select * from [User] where UserId=@UserId";
            return _connection.Query<TEntity>(sSqlCmd, commandType: CommandType.Text);
        }
        public void Add(TEntity entity)
        {

        }
        public void Delete(TEntity entity)
        {

        }
        public void Update(TEntity entity)
        {

        }
    }
}

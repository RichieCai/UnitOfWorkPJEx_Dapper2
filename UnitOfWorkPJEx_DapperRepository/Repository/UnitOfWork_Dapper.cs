using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UnitOfWork_Dapper : IUnitOfWork_Dapper
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;

        public UnitOfWork_Dapper (IDbConnectionFactory dbConnectionFactory)
        {
            _connection = dbConnectionFactory.CreateConnection();

            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                _transaction?.Commit();
            }
            catch(Exception ex)
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }


        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}

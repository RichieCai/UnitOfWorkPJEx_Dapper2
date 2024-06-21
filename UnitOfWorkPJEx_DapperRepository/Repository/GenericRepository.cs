using Dapper;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;


namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal IDbConnection _connection;
        internal IDbTransaction _transaction;
        protected GenericRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public  TEntity? GetById<TEntity>(int id,string sTableName=null)
        {
            var parameters = new DynamicParameters();
            string sNewTableName=(string.IsNullOrEmpty(sTableName))? typeof(TEntity).Name : sTableName;

            parameters.Add("UserId", id);
            var sSqlCmd = @$"select * from [{sNewTableName}] where UserId=@UserId";
            return _connection.Query<TEntity>(sSqlCmd).FirstOrDefault();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _connection.QuerySingleOrDefaultAsync<TEntity>(
                $"SELECT * FROM [{typeof(TEntity).Name}] WHERE Id = @Id", new { Id = id }, _transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>(string sTableName = null)
        {
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;
            var sSqlCmd = @$"select * from [{sNewTableName}] ";
            return await _connection.QueryAsync<TEntity>(sSqlCmd);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _connection.QueryAsync<TEntity>(
                $"SELECT * FROM {typeof(TEntity).Name}", transaction: _transaction);
        }

        public async Task AddAsync(TEntity entity)
        {
            var sql = $"INSERT INTO {typeof(TEntity).Name} (Name, Price) VALUES (@Name, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _connection.QuerySingleAsync<int>(sql, entity, _transaction);
            var propertyInfo = typeof(TEntity).GetProperty("Id");
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(entity, id);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var sql = $"UPDATE {typeof(TEntity).Name} SET Name = @Name, Price = @Price WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, entity, _transaction);
        }

        public async Task DeleteAsync(int id)
        {
            var sql = $"DELETE FROM {typeof(TEntity).Name} WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id }, _transaction);
        }
    }
}

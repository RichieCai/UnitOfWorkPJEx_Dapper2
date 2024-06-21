using Dapper;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal IUnitOfWork_Dapper _unitOfWork;
        public GenericRepository(IUnitOfWork_Dapper unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public  TEntity? GetById<TEntity>(int id,string sWhereColumn = null, string sTableName = null)
        {
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;

            string sSqlCmd = "";
            var parameters = new DynamicParameters();
            if (sWhereColumn != null)
            {
                sSqlCmd = @$"select * from [{sNewTableName}] where [{sWhereColumn}] = @Id";
                parameters.Add("Id", id);
            }
            else
            {
                sSqlCmd = @$"select * from [{sNewTableName}] where Id = @Id";
                parameters.Add("Id", id);
            }
            return _unitOfWork.Connection.QuerySingleOrDefault<TEntity>(sSqlCmd, parameters, _unitOfWork.Transaction);
        }
        public async Task<TEntity> GetByIdAsync(int id, string sWhereColumn = null, string sTableName = null)
        {
            //return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<TEntity>(
            //    $"SELECT * FROM [{typeof(TEntity).Name}] WHERE Id = @Id", new { Id = id }, _unitOfWork.Transaction);
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;
            string sSqlCmd = "";
            var parameters = new DynamicParameters();
            if (sWhereColumn != null)
            {
                sSqlCmd = @$"select * from [{sNewTableName}] where [{sWhereColumn}] = @Id";
                parameters.Add("Id", id);
            }
            else
            {
                sSqlCmd = @$"select * from [{sNewTableName}] where Id = @Id";
                parameters.Add("Id", id);
            }
            return await _unitOfWork.Connection.QuerySingleOrDefaultAsync<TEntity>(sSqlCmd, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>(string sTableName = null)
        {
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;
            var sSqlCmd = @$"select * from [{sNewTableName}] ";
            return await _unitOfWork.Connection.QueryAsync<TEntity>(sSqlCmd, transaction: _unitOfWork.Transaction);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.Connection.QueryAsync<TEntity>(
                $"SELECT * FROM [{typeof(TEntity).Name}]", transaction: _unitOfWork.Transaction);
        }

        public async Task<int> AddAsync(TEntity entity, string sInsertColumn = null)
        {
            //var sql = $"INSERT INTO [{typeof(TEntity).Name}] (Name, Price) VALUES (@Name, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            var sql = GenerateInsertQuery(typeof(TEntity).Name, entity, sInsertColumn);
            var id = await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
            return id;
        }
        public async Task AddAsyncReturnId(TEntity entity, string sInsertColumn = null)
        {
            //var sql = $"INSERT INTO [{typeof(TEntity).Name}] (Name, Price) VALUES (@Name, @Price); SELECT CAST(SCOPE_IDENTITY() as int);";
            var sql = GenerateInsertQuery(typeof(TEntity).Name, entity, sInsertColumn) + "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _unitOfWork.Connection.QuerySingleAsync<int>(sql, entity, _unitOfWork.Transaction);
            sInsertColumn = sInsertColumn ?? "Id";
            var propertyInfo = typeof(TEntity).GetProperty(sInsertColumn);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(entity, id);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            //var sql = $"UPDATE [{typeof(TEntity).Name}] SET Name = @Name, Price = @Price WHERE Id = @Id";
            var sql = GenerateUpdateQuery(typeof(TEntity).Name, entity);
            await _unitOfWork.Connection.ExecuteAsync(sql, entity, _unitOfWork.Transaction);
        }

        public async Task DeleteAsync(int id, string sWhereColumn = null, string sTableName = null)
        {
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;
            string sSqlCmd = "";
            var parameters = new DynamicParameters();
            if (sWhereColumn != null)
            {
                sSqlCmd = $"DELETE FROM {typeof(TEntity).Name} WHERE [{sWhereColumn}] = @Id";
                parameters.Add("Id", id);
            }
            else
            {
                sSqlCmd = $"DELETE FROM {typeof(TEntity).Name} WHERE Id = @Id";
                parameters.Add("Id", id);
            }
            var sql = $"DELETE FROM {typeof(TEntity).Name} WHERE Id = @Id";
            await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }
        private string GenerateInsertQuery(string tableName, TEntity entity, string sInsertColumn = null)
        {
            sInsertColumn = sInsertColumn ?? "Id";
            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != sInsertColumn).ToList();
            var columnNames = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            return $"INSERT INTO [{tableName}] ({columnNames}) VALUES ({values})";
        }

        private string GenerateUpdateQuery(string tableName, TEntity entity)
        {
            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "Id").ToList();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            return $"UPDATE [{tableName}] SET {setClause} WHERE Id = @Id";
        }

    }
}

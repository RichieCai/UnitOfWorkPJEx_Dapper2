using Dapper;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{

    public class UserUnitRepository :  IUserUnitRepository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;
        public UserUnitRepository(IDbTransaction transaction) 
        {
            _transaction = transaction;
            _connection = transaction.Connection;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _connection.QueryAsync<User>("select * from [User] ", transaction: _transaction);
        }
    }
}

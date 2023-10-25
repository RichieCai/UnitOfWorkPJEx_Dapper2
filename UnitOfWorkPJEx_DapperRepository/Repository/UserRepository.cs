using Dapper;
using Generic.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMsDBConn _msDBConn;
        public UserRepository(IMsDBConn msDBConn)
        {
            _msDBConn = msDBConn;
        }

        public async Task<T> GetById<T>(int UserId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("UserId", UserId);
            string sCmd = @"select * from [User] where UserId=@UserId";
            var User = await _msDBConn.QueryAsync<T>(sCmd, parameter);
            return User.FirstOrDefault();

        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            string sCmd = @"select * from [User] ";
            var Userlist = await _msDBConn.QueryAsync<T>(sCmd);
            return Userlist;
        }

        public async Task<bool> Add(User user)
        {

            string sSqlCmd = @"
                Insert Into [User]([UserName],[Age],[Sex],[CountryId],[CityId]) 
                values(@UserName,@Age,@Sex,@CountryId,@CityId) ";
            var parameter = new DynamicParameters();
            parameter.Add("UserName", user.UserName);
            parameter.Add("Age", user.Age);
            parameter.Add("Sex", user.Sex);
            parameter.Add("CountryId", user.CountryId);
            parameter.Add("CityId", user.CityId);

            int iResult = _msDBConn.Excute(sSqlCmd, parameter);

            return (iResult > 0) ? true : false;

        }
        public async Task<bool> Update(User user)
        {
            var parameter = new DynamicParameters();
            parameter.Add("UserName", user.UserName);
            parameter.Add("Age", user.Age);
            parameter.Add("Sex", user.Sex);
            parameter.Add("CountryId", user.CountryId);
            parameter.Add("CityId", user.CityId);
            parameter.Add("UserId", user.UserId);

            string sSqlCmd = @"
                    Update [User]
                    set UserName=@UserName,Age=@Age,Sex=@Sex,CountryId=@CountryId,CityId=@CityId
                    where UserId=@UserId  ";

            var command = new CommandDefinition(sSqlCmd, parameter, flags: CommandFlags.Buffered);
            var sql = command.CommandText; // 此處獲取 SQL 查詢
            var sqlParameters = command.Parameters; // 此處獲取參數的集合

            int iResult = _msDBConn.Excute(sSqlCmd, parameter);

            return (iResult > 0) ? true : false;
        }
        public async Task<bool> Delete(int iUserId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("UserId", iUserId);

            string sSqlCmd = @"
                    delete [User]
                    where UserId=@UserId  ";
            int iResult = _msDBConn.Excute(sSqlCmd, parameter);
            return (iResult > 0) ? true : false;
        }
    }
}

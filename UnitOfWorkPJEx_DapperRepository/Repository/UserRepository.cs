using Dapper;
using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMsDBConn _msDBConn;
        public UserRepository(IMsDBConn msDBConn)
        {
            _msDBConn = msDBConn;
        }

        public async Task<T?> GetById<T>(string UserId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("UserId", UserId);
            string sCmd = @"select * from [User] where UserId=@UserId";
            return await _msDBConn.QuerySingleAsync<T>(sCmd, parameter);
        }

        public async Task<ResultVM<UserVM>> Get<UserVM>(UserInput input)
        {
            ResultVM<UserVM> result = new ResultVM<UserVM>();
            var parameter = new DynamicParameters();
            string sSelectCount = "select COUNT(*) total";
            string sSelect = "select * ";
            string sCmd = @" from [User] where 1=1 ";
            if (input.UserId>0)
            {
                parameter.Add("UserId", input.UserId);
                sCmd += "and UserId=@UserId";
            }
            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                parameter.Add("UserName", input.UserName);
                sCmd += "and UserName=@UserName";
            }
            if (input.Age > 0)
            {
                parameter.Add("UserName", input.Age);
                sCmd += "and Age=@Age";
            }
            if (!string.IsNullOrWhiteSpace(input.CityId))
            {
                parameter.Add("UserName", input.CityId);
                sCmd += "and CityId=@CityId";
            }
            if (input.CountryId > 0)
            {
                parameter.Add("UserName", input.CountryId);
                sCmd += "and CountryId=@CountryId";
            }

            var vTotal = await _msDBConn.ExecuteScalarAsync<int>(sSelectCount + sCmd, parameter);
            result.TotalRecords = vTotal;
            if (vTotal == 0)
                return result;
            
            string sOrder = $@"
                        order by UserId
                        offset {(input.Page - 1) * input.PageSize} rows fetch next {input.PageSize} rows only;
                        ";

            sCmd = sSelect + sCmd + sOrder;
            var UserData = await _msDBConn.QueryAsync<UserVM>(sCmd, parameter);
            result.Data = UserData.ToList();
            return result;
        }


        public async Task<IEnumerable<T>> GetAll<T>()
        {
            string sCmd = @"select * from [User] ";
            var Userlist = await _msDBConn.QueryAsync<T>(sCmd);
            return Userlist;
        }
        //public virtual IEnumerable<T> Get<T>(  Expression<Func<T, bool>> filter = null,
        //   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //   string includeProperties = "")
        //{
        //    IQueryable<T> query = dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }

        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }

        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    else
        //    {
        //        return query.ToList();
        //    }
        //}

        public async Task<bool> AddAsync(User user)
        {
            List<string> NotMatchList = new List<string>();
            NotMatchList.Add("UserId");
            int iResult = await _msDBConn.AddAsync(user, NotMatchList);
            return (iResult > 0) ? true : false;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            string[] setCol = new string[] { "UserName", "Age", "Sex", "CountryId", "CityId" };
            string[] ConditionCol = new string[] { "UserId" };
            //int iResult = await _msDBConn.UpdateAsync<User>( setCol, user, ConditionCol, user);

            int iResult = await _msDBConn.UpdateAsync<User>(setCol, user, ConditionCol, user);
            return (iResult > 0) ? true : false;
        }

        public  bool Update(User user)
        {
            string[] setCol = new string[] { "UserName", "Age", "Sex", "CountryId", "CityId" };
            string[] ConditionCol = new string[] { "UserId" };
            int iResult =  _msDBConn.Update<User>(setCol, user, ConditionCol, user);

            return (iResult > 0) ? true : false;
        }

        public async Task<bool> DeleteAsync(User user)
        {
           // User user=await GetById<User>(iUserId);
            int iResult = await _msDBConn.DeleteAsync<User>(user);
            return (iResult > 0) ? true : false;
        }
    }
}

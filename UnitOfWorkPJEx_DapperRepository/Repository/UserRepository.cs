using Dapper;
using Generic.Interface;
using System.Linq.Expressions;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

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
            var User = await _msDBConn.QueryAsync<T>(sCmd, parameter);
            return User.FirstOrDefault();

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

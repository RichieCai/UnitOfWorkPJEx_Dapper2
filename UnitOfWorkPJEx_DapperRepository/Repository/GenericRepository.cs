using Dapper;
using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;


namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IMsDBConn _msDBConn;
        protected GenericRepository(IMsDBConn msDBConn)
        {
            _msDBConn = msDBConn;
        }
        public TEntity? GetById<TEntity>(int id,string sTableName=null,string sConditionCol=null)
        {
            var parameters = new DynamicParameters();
            string sNewTableName=(string.IsNullOrEmpty(sTableName))? typeof(TEntity).Name : sTableName;

            parameters.Add("UserId", id);
            var sSqlCmd = @$"select * from [{sNewTableName}] where UserId=@UserId";
            return _msDBConn.Query<TEntity>(sSqlCmd).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(string sTableName = null)
        {
            string sNewTableName = (string.IsNullOrEmpty(sTableName)) ? typeof(TEntity).Name : sTableName;
            var sSqlCmd = @$"select * from [{sNewTableName}] ";
            return _msDBConn.Query<TEntity>(sSqlCmd);
        }
        public async Task<bool> Add(TEntity entity)
        {
            return false;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            return false;
        }
        public async Task<bool> Update(TEntity entity)
        {
            return false;
        }
    }
}

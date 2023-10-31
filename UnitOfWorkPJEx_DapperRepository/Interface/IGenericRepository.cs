namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity? GetById<TEntity>(int id, string sTableName = null, string sConditionCol = null);
        IEnumerable<TEntity> GetAll<TEntity>(string sTableName = null);
        Task<bool> Add(TEntity entity);
        Task<bool> Delete(TEntity entity);
        Task<bool> Update(TEntity entity);
    }
}

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity? GetById<TEntity>(int id, string sWhereColumn = null, string sTableName = null);

        Task<TEntity> GetByIdAsync(int id, string sWhereColumn = null, string sTableName = null);
        Task<IEnumerable<TEntity>> GetAll<TEntity>(string sTableName = null);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity, string sInsertColumn = null);
        Task AddAsyncReturnId(TEntity entity, string sInsertColumn = null);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id, string sWhereColumn = null, string sTableName = null);
    }
}

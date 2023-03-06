namespace Students.API.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        void Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Update(TEntity dbEntity, TEntity entity);
        void Save();
    }
}
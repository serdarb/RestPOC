namespace RestPOC.Domain {
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize);

        T GetSingle(int id);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
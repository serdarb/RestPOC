namespace RestPOC.Domain
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        readonly DbContext entitiesContext;
        public EntityRepository(DbContext entitiesContext)
        {
            if (entitiesContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }

            this.entitiesContext = entitiesContext;
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.entitiesContext.Set<T>();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.entitiesContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public T GetSingle(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.entitiesContext.Set<T>().Where(predicate);
        }

        public virtual PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize)
        {
            IQueryable<T> query = this.GetAll().OrderByDescending(x=>x.Id);
            return query.ToPaginatedList(pageIndex, pageSize);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = this.entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = this.entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            entity.DeletedOn = DateTime.Now;
            this.Update(entity);
        }

        public virtual void Save()
        {
            this.entitiesContext.SaveChanges();
        }
    }

}

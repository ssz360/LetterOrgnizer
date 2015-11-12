namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Implements the generic <see cref="IRepository"/> interface.
    /// </summary>
    /// <typeparam name="T">A class derived from <see cref="EntityBase" class./></typeparam>
    public class Repository<T> : IRepository<T> where T : Database
    {
        #region private fields
        /// <summary>
        /// Underlying DbSet.
        /// </summary>
        private DbSet<T> dbSet;

        /// <summary>
        /// Underlying DbContext
        /// </summary>
        private DbContext dbContext;

        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public Repository(DbContext dataContext)
        {
            this.dbContext = dataContext;
            this.dbSet = this.dbContext.Set<T>();
        }
        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the database set.
        /// </summary>
        /// <value>
        /// The database set.
        /// </value>
        protected DbSet<T> DbSet
        {
            get
            {
                return this.dbSet;
            }

            set
            {
                this.dbSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected DbContext DbContext
        {
            get
            {
                return this.dbContext;
            }

            set
            {
                this.dbContext = value;
            }
        }
        #endregion

        // _________________________________________________________________________________________
        #region methods

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public T Insert(T entity)
        {
            return this.dbSet.Add(entity);
        }

        /// <summary>
        /// Returns an entity based on primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <returns>
        /// The matched entity
        /// </returns>
        public T GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>
        /// Returns an IEnumerable based on the query, order clause and the properties included
        /// </summary>
        /// <param name="query">Link query for filtering.</param>
        /// <param name="orderBy">Link query for sorting.</param>
        /// <param name="includeProperties">Navigation properties separated by comma for eager loading.</param>
        /// <returns>IEnumerable containing the resulting entity set.</returns>
        public System.Collections.Generic.IEnumerable<T> GetByQuery(Expression<Func<T, bool>> query = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> queryResult = this.dbSet;

            // If there is a query, execute it against the dbset
            if (query != null)
            {
                queryResult = queryResult.Where(query);
            }

            // get the include requests for the navigation properties and add them to the query result
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                queryResult = queryResult.Include(property);
            }

            // if a sort request is made, order the query accordingly.
            // if not, return the results as is
            if (orderBy != null)
            {
                return orderBy(queryResult).ToList();
            }
            else
            {
                return queryResult.ToList();
            }
        }

        /// <summary>
        /// Gets and returns the first matching entity based on the query..
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// The matched entity
        /// </returns>
        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.First(predicate);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Updates an entity by primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        public void UpdateById(int id)
        {
            T entity = this.dbSet.Find(id);
            this.dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public T Delete(T entity)
        {
            if (this.dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            return this.dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        public T DeleteByID(int id)
        {
            T entity = this.dbSet.Find(id);
           return this.dbSet.Remove(entity);
        }
        public IEnumerable<T> GetAll()
        {
            return this.dbSet as IEnumerable<T>;
        }

        #endregion

    }
}

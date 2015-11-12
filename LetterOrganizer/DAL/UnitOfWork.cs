namespace LetterOrganizer
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The unit of work class
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        // _________________________________________________________________________________________
        #region private fields

        /// <summary>
        /// The database context
        /// </summary>
        private DbContext dbContext = null;

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The letter repository
        /// </summary>
        private IRepository<Letter> letterRepository = null;

        /// <summary>
        /// The ring binder repository
        /// </summary>
        private IRepository<RingBinder> ringBinderRepository = null;
        
        #endregion

        // _________________________________________________________________________________________
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UnitOfWork(DbContext context)
        {
            this.dbContext = context;
        }

        #endregion

        // _________________________________________________________________________________________
        #region Properties

        /// <summary>
        /// Gets the letter repository.
        /// </summary>
        /// <value>
        /// The letter repository.
        /// </value>
        public IRepository<Letter> LetterRepository
        {
            get
            {
                if (this.letterRepository == null)
                {
                    this.letterRepository = new Repository<Letter>(this.dbContext);
                }

                return this.letterRepository;
            }
        }

        /// <summary>
        /// Gets the ring binder repository.
        /// </summary>
        /// <value>
        /// The ring binder repository.
        /// </value>
        public IRepository<RingBinder> RingBinderRepository
        {
            get
            {
                if (this.ringBinderRepository == null)
                {
                    this.ringBinderRepository = new Repository<RingBinder>(this.dbContext);
                }

                return this.ringBinderRepository;
            }
        }
        

        #endregion

        // _________________________________________________________________________________________
        #region Public Methods

        // _________________________________________________________________________________________
        #region IDisposable Overrides

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Used to save the changes to the underlying data store.
        /// </summary>
        public virtual void Commit()
        {
            this.dbContext.SaveChanges();
        }
        public virtual IRepository<T> GetRepository<T>() where T : Database
        {
            var t = this.GetType().GetProperties();
            IRepository<T> result = null;

            foreach (var item in t)
            {
                if (item.PropertyType == typeof(IRepository<T>))
                {
                    result = item.GetValue(this) as IRepository<T>;
                }
            }

            return result;
        }

        public bool ContextHasChanges
        {

            get
            {
                return this.dbContext.ChangeTracker
                            .Entries()
                            .Any(x => x.State == EntityState.Added ||
                                      x.State == EntityState.Deleted ||
                                      x.State == EntityState.Modified);
            }
        }

        #endregion

        // _________________________________________________________________________________________
        #region Protected Methods
        // http://www.codeproject.com/Articles/319826/IDisposable-Finalizer-and-SuppressFinalize-in-Csha

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.dbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion
    }
}

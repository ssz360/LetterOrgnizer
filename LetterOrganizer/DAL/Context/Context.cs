namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// context of the entity framework
    /// </summary>
    public class Context : DbContext
    {
        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
            : base("Context")
        {
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the letters.
        /// </summary>
        /// <value>
        /// The letters.
        /// </value>
        public DbSet<Letter> Letters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ring binders.
        /// </summary>
        /// <value>
        /// The ring binders.
        /// </value>
        public DbSet<RingBinder> RingBinders
        {
            get;
            set;
        }
        #endregion
    }
}

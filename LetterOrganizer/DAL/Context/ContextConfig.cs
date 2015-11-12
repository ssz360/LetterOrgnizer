namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// context configuration class
    /// </summary>
    public class ContextConfig : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextConfig"/> class.
        /// </summary>
        public ContextConfig()
        {
#if DEBUG
            this.SetDatabaseInitializer<Context>(new ContextDebugInitializer());
#else
                    SetDatabaseInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
#endif
        }
    }
}

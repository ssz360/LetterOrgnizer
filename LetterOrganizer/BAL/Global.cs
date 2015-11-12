namespace LetterOrganizer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// the class contains global settings and fileds
    /// </summary>
    public class Global
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private static UnitOfWork unitOfWork;

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public static UnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    InitUnitOfWork();
                }

                return Global.unitOfWork;
            }
        }

        /// <summary>
        /// Initializes the unit of work.
        /// </summary>
        private static void InitUnitOfWork()
        {
            unitOfWork = new UnitOfWork(new Context());
        }
    }
}

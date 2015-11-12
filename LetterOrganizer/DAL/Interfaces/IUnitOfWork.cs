using System;

namespace LetterOrganizer
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : Database;

        bool ContextHasChanges
        {
            get;
        }
    }
}

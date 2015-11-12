using SSZ.Accounting.Entities;
using System;
namespace SSZ.Accounting.DAL
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : BaseModel;

        bool ContextHasChanges
        {
            get;
        }
    }
}

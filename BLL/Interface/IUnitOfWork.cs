using System;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();// returns the number of changes to our database
    }
}

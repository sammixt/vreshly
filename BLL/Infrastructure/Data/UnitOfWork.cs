using System;
using System.Collections;
using System.Threading.Tasks;
using BLL.Data;
using BLL.Entities;
using BLL.Interface;

namespace BLL.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable _repositories; // all repository used in UoW will be stored in the hash table

        public UnitOfWork(StoreContext _context)
        {
            context = _context;
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

                _repositories.Add(type, repositoryInstance);
                
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}

using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkSample.DataAccessLayer
{
    public interface ISampleDatabase
    {
        IQueryable<T> Get<T>() where T : class, IEntity;

        T Add<T>(T entity) where T : class, IEntity;

        T Remove<T>(T entity) where T : class, IEntity;

        Task<int> CommitAsync(CancellationToken token);
        
        Task<int> CommitAsync();
    }
}
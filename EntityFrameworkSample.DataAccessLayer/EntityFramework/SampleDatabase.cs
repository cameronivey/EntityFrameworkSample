using EntityFrameworkSample.DataAccessLayer;
using EntityFrameworkSample.Domain;
using EntityFrameworkSample.Domain.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EntityFrameworkSample.DataAccessLayer.EntityFramework
{
    public class SampleDatabase : DbContext, ISampleDatabase
    {
        public SampleDatabase() : base()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        //Optionally Mask the EF DbSet with IQueryable - gain abstraction with the cost of functionality
        public IQueryable<T> Get<T>() where T : class, IEntity
        {
            return this.Set<T>();
        }

        public T Add<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Add(entity);
        }

        public T Remove<T>(T entity) where T : class, IEntity
        {
            return this.Set<T>().Remove(entity);
        }

        public async Task<int> CommitAsync()
        {
            return await this.CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken token)
        {
            try
            {
                return await this.SaveChangesAsync(token); 
            }
            catch (DbEntityValidationException ex)
            {
                // We would log the exception here
                throw ex;
            }
            catch (DbUpdateException ex)
            {
                // We would log the exception here
                throw ex;
            }
            catch (Exception ex)
            {
                // We would log the exception here
                throw ex;
            }
        }
    }
}

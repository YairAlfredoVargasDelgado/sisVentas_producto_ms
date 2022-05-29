using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace SysVentas.Products.Infrastructure.Data.Base
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        void SetModified(object entity);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void DoDispose();
    }

    public class DbContextBase : DbContext, IDbContext
    {
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public void DoDispose()
        {
            base.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payroll.Core.BaseModels;
using Payroll.Core.Constants;
using Payroll.Model.Extensions;

namespace Payroll.Model.Context
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options) { }
        public override int SaveChanges()
        {
            SetAuditableEntities();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetAuditableEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IDeletableEntityFilter).IsAssignableFrom(type.ClrType))
                    continue;

                if (typeof(IDeletableEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
            base.OnModelCreating(modelBuilder);
        }
        private void SetAuditableEntities()
        {
            SetCreatableEntities();

            SetUpdatableEntities();

            SetDeletableEntities();
        }

        private void SetDeletableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IDeletableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Deleted = false;
                        entry.Entity.DeletedDate = null;
                        entry.Entity.DeletedBy = null;
                        break;
                    case EntityState.Modified:
                        if (typeof(IDeletableEntityFilter).IsAssignableFrom(entry.Entity.GetType()))
                        {
                            if (entry.Entity.Deleted is false)
                            {
                                entry.Entity.DeletedDate = null;
                                entry.Entity.DeletedBy = null;
                            }
                            else if (!entry.Entity.DeletedDate.HasValue)
                                goto case EntityState.Deleted;
                            else
                            {
                                entry.Property(x => x.DeletedDate).IsModified = false;
                                entry.Property(x => x.DeletedBy).IsModified = false;
                            }
                        }
                        else
                        {
                            entry.Property(x => x.Deleted).IsModified = false;
                            entry.Property(x => x.DeletedDate).IsModified = false;
                            entry.Property(x => x.DeletedBy).IsModified = false;
                        }
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.Deleted = true;
                        entry.Entity.DeletedDate = DateTimeOffset.UtcNow;
                        entry.Entity.DeletedBy = AuthConst.DEFAULT_USER;
                        break;

                    default:
                        break;
                }
            }
        }

        private void SetUpdatableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IUpdatebleEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Updated = false;
                        entry.Entity.UpdatedDate = null;
                        entry.Entity.UpdatedBy = null;
                        break;

                    case EntityState.Modified:
                        entry.Entity.Updated = true;
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.UpdatedBy = AuthConst.DEFAULT_USER;
                        break;

                    case EntityState.Deleted:
                        entry.Property(x => x.Updated).IsModified = false;
                        entry.Property(x => x.UpdatedDate).IsModified = false;
                        entry.Property(x => x.UpdatedBy).IsModified = false;
                        break;

                    default:
                        break;
                }
            }
        }

        private void SetCreatableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<ICreatebleEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        entry.Entity.CreatedBy = AuthConst.DEFAULT_USER;
                        break;

                    case EntityState.Modified:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
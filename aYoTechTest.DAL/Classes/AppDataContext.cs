using aYoTechTest.BR.Models.Interfaces;
using aYoTechTest.CommonLibraries.Interfaces;
using aYoTechTest.DAL.EntityDbConfigurations;
using aYoTechTest.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace aYoTechTest.DAL.Classes
{
    public class AppDataContext : DbContext
    {
        public readonly DbContextOptions<AppDataContext> _options;
        public readonly ICurrentUserHelper _currentUserInfo;

        public AppDataContext(
            DbContextOptions<AppDataContext> options,
            ICurrentUserHelper currentUserInfo
        ) : base(options)
        {
            _options = options;
            _currentUserInfo = currentUserInfo;
        }

        public DbSet<MeasuringUnit> MeasuringUnits { get; set; }
        public DbSet<SupportedConversion> SupportedConversions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MeasuringUnitDbConfig());
            modelBuilder.ApplyConfiguration(new SupportedConversionDbConfig());


            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuditInfo();

            return await base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            AddAuditInfo();

            return base.SaveChanges();
        }


        public void AddAuditInfo()
        {
            var modifiedEntities = ChangeTracker.Entries().Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted) && e.Entity is IEntityBase);
            string CurrentUserId = _currentUserInfo.UserId();
            if (modifiedEntities.Count() > 0)
            {
                foreach (var entry in modifiedEntities)
                {
                    var entity = (IEntityBase)entry.Entity;
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedById = CurrentUserId;
                        entity.CreatedAt = now;

                        base.Entry(entity).Property(x => x.LastUpdatedById).IsModified = false;
                        base.Entry(entity).Property(x => x.LastUpdatedAt).IsModified = false;
                        base.Entry(entity).Property(x => x.DeletedById).IsModified = false;
                        base.Entry(entity).Property(x => x.DeletedAt).IsModified = false;

                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.LastUpdatedById = CurrentUserId;
                        entity.LastUpdatedAt = now;

                        base.Entry(entity).Property(x => x.CreatedById).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                        base.Entry(entity).Property(x => x.DeletedById).IsModified = false;
                        base.Entry(entity).Property(x => x.DeletedAt).IsModified = false;

                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        if (entity.DeletedById == null)
                        {
                            entry.State = EntityState.Modified;
                            entity.DeletedById = CurrentUserId;
                            entity.DeletedAt = now;

                            base.Entry(entity).Property(x => x.CreatedById).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                            base.Entry(entity).Property(x => x.LastUpdatedById).IsModified = false;
                            base.Entry(entity).Property(x => x.LastUpdatedAt).IsModified = false;
                        }
                    }
                };
            }
        }
    }
}

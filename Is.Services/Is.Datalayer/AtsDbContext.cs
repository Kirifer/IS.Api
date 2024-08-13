using Is.Core.Database;
using Is.Core.Database.Abstraction;
using Is.Core.Database.Abstraction.Interface;
using Is.Datalayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace Is.Datalayer
{
    public class AtsDbContext : DbContextBase
    {
        public DbSet<BudgetExpenses> BudgetExpenses { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Supplies> Supplies { get; set; }

        public DbSet<SupplyCodes> SupplyCodes { get; set; }

        public AtsDbContext(DbContextOptions<DbContextBase> options)
           : base(options)
        {
        }

        public AtsDbContext(IDbConfig dbConfig, IDbUserContext dbUserContext, IDbMigration dbMigration)
            : base(dbConfig, dbUserContext, dbMigration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

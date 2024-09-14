using GkoTradeService.Models;
using Microsoft.EntityFrameworkCore;

namespace GkoTradeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
        }

        public DbSet<CryptoPrice> CryptoPrices { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AuditInfo();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Set the modified and created date time values during save changes
        /// </summary>
        private void AuditInfo()
        {
            foreach (var entry in ChangeTracker.Entries<dynamic>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Modified = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.Created = DateTime.UtcNow;
                }
            }
        }
    }
}
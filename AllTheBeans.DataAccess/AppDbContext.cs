using AllTheBeans.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AllTheBeans.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bean> Beans => Set<Bean>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<BeanImage> BeanImages => Set<BeanImage>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bean>().HasIndex(b => b.ImportId).IsUnique();

            modelBuilder.Entity<Bean>()
                .HasOne(b => b.Country)
                .WithMany(c => c.Beans)
                .HasForeignKey(b => b.CountryId);

            modelBuilder.Entity<Bean>()
                .HasOne(b => b.Image)
                .WithMany(i => i.Beans)
                .HasForeignKey(b => b.ImageId);
        }
    }

}

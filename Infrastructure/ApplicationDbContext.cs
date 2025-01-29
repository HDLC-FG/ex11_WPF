using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using ApplicationCore.Models;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Option> Options { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbConnection existingConnection, bool connectionDisposeWithContext) : base(existingConnection, connectionDisposeWithContext)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasRequired(v => v.Engine);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Options)
                .WithMany();

            //modelBuilder.Entity<Vehicle>()
            //    .HasMany(v => v.Options)
            //    .WithMany(o => o.Vehicles);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var conn = ConfigurationManager.ConnectionStrings["GarageConnection"].ConnectionString;
        //    optionsBuilder.UseSqlServer(conn);
        //}
    }
}

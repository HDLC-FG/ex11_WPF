using System.Data.Common;
using System.Data.Entity;
using ApplicationCore.Models;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Chassis> Chassis { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Option> Options { get; set; }

        public ApplicationDbContext() : base("Garage")
        {
        }

        public ApplicationDbContext(DbConnection existingConnection, bool connectionDisposeWithContext) : base(existingConnection, connectionDisposeWithContext)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id)
                .Property(v => v.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Vehicle>()
                .HasRequired(v => v.Chassis);

            modelBuilder.Entity<Vehicle>()
                .HasRequired(v => v.Engine);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Options)
                .WithMany();
        }
    }
}

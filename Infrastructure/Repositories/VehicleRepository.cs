using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public VehicleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Vehicle>> GetAll()
        {
            return await dbContext.Vehicles.Include(v => v.Engine).Include(v => v.Options).ToListAsync();
        }

        public async Task Update(Vehicle vehicle)
        {
            dbContext.Entry(vehicle).State = EntityState.Modified;
            foreach (var option in vehicle.Options)
            {
                //This is needed because entity framework by default use EntityState.Added instead of Modified if option exist, so we dot it manually
                if (dbContext.Entry(option).State == EntityState.Added && await dbContext.Options.AnyAsync(o => o.Id == option.Id))
                {
                    dbContext.Entry(option).State = EntityState.Modified;
                }
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task Add(Vehicle vehicle)
        {
            dbContext.Vehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();
        }
    }
}

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
            return await dbContext.Vehicles
                .Include(v => v.Chassis)
                .Include(v => v.Engine)
                .Include(v => v.Options)
                .ToListAsync();
        }

        public async Task<Vehicle> GetById(int id)
        {
            return await dbContext.Vehicles.FindAsync(id);
        }

        public async Task Add(Vehicle vehicle)
        {
            dbContext.Vehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Vehicle vehicle)
        {
            dbContext.Entry(vehicle).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Vehicle vehicle)
        {
            dbContext.Vehicles.Remove(vehicle);
            await dbContext.SaveChangesAsync();
        }
    }
}

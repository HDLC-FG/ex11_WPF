using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using System.Data.Entity;

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
            return await dbContext.Vehicles.Include(v => v.Engine).ToListAsync();
        }

        public async Task Add(Vehicle vehicle)
        {
            dbContext.Vehicles.Add(vehicle);
            await dbContext.SaveChangesAsync();
        }
    }
}

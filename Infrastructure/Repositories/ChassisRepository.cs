using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public class ChassisRepository : IChassisRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ChassisRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Chassis>> GetAll()
        {
            return await dbContext.Chassis.ToListAsync();
        }
    }
}

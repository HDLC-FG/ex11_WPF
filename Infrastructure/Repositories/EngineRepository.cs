using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class EngineRepository : IEngineRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EngineRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Engine>> GetAll()
        {
            return await dbContext.Engines.ToListAsync();
        }

        public async Task<Engine> GetById(int id)
        {
            return await dbContext.Engines.FindAsync(id);
        }
    }
}

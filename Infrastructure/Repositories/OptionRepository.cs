using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OptionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IList<Option>> GetAll()
        {
            return await dbContext.Options.ToListAsync();
        }
    }
}

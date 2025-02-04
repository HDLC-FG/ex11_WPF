using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;

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
        
        public async Task<IList<Option>> GetAvailables(IList<Option> optionsNotAvailable)
        {
            var idsToExclude = optionsNotAvailable.Select(x => x.Id).ToList();
            return await dbContext.Options.Where(o => !idsToExclude.Contains(o.Id)).ToListAsync();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace Infrastructure.Repositories
{
    public interface IOptionRepository
    {
        Task<IList<Option>> GetAvailables(IList<Option> optionsNotAvailable);
    }
}
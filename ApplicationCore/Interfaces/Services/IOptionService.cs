using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface IOptionService
    {
        Task<IList<Option>> GetAll();
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface IEngineService
    {
        Task<IList<Engine>> GetAll();
    }
}
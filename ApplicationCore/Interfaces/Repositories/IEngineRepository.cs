using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IEngineRepository
    {
        Task<IList<Engine>> GetAll();
        Task<Engine> GetById(int id);
    }
}
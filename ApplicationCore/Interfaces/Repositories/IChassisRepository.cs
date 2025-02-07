using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IChassisRepository
    {
        Task<IList<Chassis>> GetAll();
    }
}
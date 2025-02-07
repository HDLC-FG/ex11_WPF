using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface IChassisService
    {
        Task<IList<Chassis>> GetAll();
    }
}

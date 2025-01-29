using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<IList<Vehicle>> GetAll();
        Task Add(Vehicle vehicle);
    }
}
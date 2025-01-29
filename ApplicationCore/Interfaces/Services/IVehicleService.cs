using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<IList<Vehicle>> GetAll();
        Task Add(Vehicle vehicle);
        Task Update(Vehicle vehicle);
    }
}
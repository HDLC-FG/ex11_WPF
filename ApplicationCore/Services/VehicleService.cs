using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<IList<Vehicle>> GetAll()
        {
            return await vehicleRepository.GetAll();
        }

        public async Task Add(Vehicle vehicle)
        {
            await vehicleRepository.Add(vehicle);
        }

        public async Task Update(Vehicle vehicle)
        {
            await vehicleRepository.Update(vehicle);
        }

        public async Task Delete(int id)
        {
            var vehicle = await vehicleRepository.GetById(id);
            if (vehicle != null)
            {
                await vehicleRepository.Delete(vehicle);
            }
            else
            {
                throw new Exception("Vehicle does not exist");
            }
        }
    }
}

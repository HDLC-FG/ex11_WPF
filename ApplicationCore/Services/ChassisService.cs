using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class ChassisService : IChassisService
    {
        private readonly IChassisRepository chassisRepository;

        public ChassisService(IChassisRepository chassisRepository)
        {
            this.chassisRepository = chassisRepository;
        }

        public async Task<IList<Chassis>> GetAll()
        {
            return await chassisRepository.GetAll();
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class EngineService : IEngineService
    {
        private readonly IEngineRepository engineRepository;

        public EngineService(IEngineRepository engineRepository)
        {
            this.engineRepository = engineRepository;
        }

        public async Task<IList<Engine>> GetAll()
        {
            return await engineRepository.GetAll();
        }
    }
}

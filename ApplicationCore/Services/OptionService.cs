using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace ApplicationCore.Services
{
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository optionRepository;

        public OptionService(IOptionRepository optionRepository)
        {
            this.optionRepository = optionRepository;
        }

        public async Task<IList<Option>> GetAll()
        {
            return await optionRepository.GetAll();
        }
    }
}

using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;
using UnitOfWorkPJEx_DapperRepository.Repository;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(IMsDBConn msDBConn, ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<Country> Get(string CountryId)
        {
           return await _countryRepository.Get<Country>(CountryId);
        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _countryRepository.GetAll<Country>();
        }

        public async Task<IEnumerable<CountryVM>> GetCountryCityAll()
        {
            return await _countryRepository.GetCountryCityAll<CountryVM>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;

namespace UnitOfWorkPJEx_DapperService.Interface
{
    public interface ICountryService
    {
        public Task<Country> Get(string CountryId);
        public Task<IEnumerable<Country>> GetAll();
        public Task<IEnumerable<CountryVM>> GetCountryCityAll();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkPJEx_DapperRepository.Models.ViewModels
{
    public class CountryVM
    {
       public string CountryId { get; set; }
       public string CountryName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
    }
}

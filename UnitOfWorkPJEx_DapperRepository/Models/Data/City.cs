using System;
using System.Collections.Generic;

namespace UnitOfWorkPJEx_DapperRepository.Models.Data
{
    public partial class City
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int? CountryId { get; set; }
        public bool? Status { get; set; }
        public int? Orderby { get; set; }
    }
}

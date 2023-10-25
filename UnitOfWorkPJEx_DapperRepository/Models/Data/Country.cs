using System;
using System.Collections.Generic;

namespace UnitOfWorkPJEx_DapperRepository.Models.Data
{
    public partial class Country
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Status { get; set; }
        public int? Orderby { get; set; }
    }
}

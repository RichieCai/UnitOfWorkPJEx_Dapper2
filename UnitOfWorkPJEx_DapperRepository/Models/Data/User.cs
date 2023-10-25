using System;
using System.Collections.Generic;

namespace UnitOfWorkPJEx_DapperRepository.Models.Data
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int? Age { get; set; }
        public byte? Sex { get; set; }
        public string? CountryId { get; set; }
        public string? CityId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitOfWorkPJEx_DapperRepository.Models.DataModels
{
    public partial class User
    {
        public int? UserId { get; set; }
        
        [Required(ErrorMessage ="使用者名稱為必填")]
        public string? UserName { get; set; }
        
        [Range(0, 120,ErrorMessage ="年齡不得超過120")]
        public int? Age { get; set; }

        [Range(1, 2, ErrorMessage = "性別為必填,1:男,2:女")]
        public eSex Sex { get; set; }

        public int? CountryId { get; set; }

        public string? CityId { get; set; }


        public enum eSex {
            male=1,
            female=2

        }
    }
}

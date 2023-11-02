using static UnitOfWorkPJEx_DapperRepository.Models.DataModels.User;

namespace UnitOfWorkPJEx_DapperRepository.Models.Input
{
    public class UserInput:BaseInput
    {
        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public int? Age { get; set; }

        public eSex? Sex { get; set; }

        public int? CountryId { get; set; }
        public string? CityId { get; set; }
    }
}

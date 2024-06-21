using AutoMapper;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
namespace UnitOfWorkPJEx_Dapper2
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UnitOfWorkPJEx_DapperRepository.Models.Data.User> ().ReverseMap();
        }
    }
}

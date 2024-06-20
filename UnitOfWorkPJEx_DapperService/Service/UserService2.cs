using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.Data;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserService2 : IUserService2
    {
        private readonly IMsDBConn _msDBConn;
        private readonly IUnitOfWork_Dapper _iunitOfWork_Dapper;

        public UserService2(IUnitOfWork_Dapper iunitOfWork_Dapper)
        {
            _iunitOfWork_Dapper = iunitOfWork_Dapper;
        }

        public async Task<IEnumerable<User>> GetUserAll()
        {
            // var userslist =  _GenrRepo.GetAll<User>();
            var userslist = await _iunitOfWork_Dapper.Users.GetAllAsync();
            return userslist;
        }
    }
}

using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Models.Input;
using UnitOfWorkPJEx_DapperRepository.Models.ViewModels;
using UnitOfWorkPJEx_DapperService.Interface;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMsDBConn _msDBConn;
        //private readonly  IGenericRepository<User> _GenrRepo;

        public UserService(IMsDBConn msDBConn, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _msDBConn = msDBConn;
            // _GenrRepo = GenrRepo;
        }

        public async Task<User?> GetById(int UserId)
        {
            if (UserId <= 0) return null;
            var User = await _userRepository.GetById<User>(UserId.ToString());
            return User;
        }

        public async Task<ResultVM<UserVM>> Get(UserInput input)
        {
            var User = await _userRepository.Get<UserVM>(input);
            return User;
        }


        public async Task<IEnumerable<User>> GetUserAll()
        {
            // var userslist =  _GenrRepo.GetAll<User>();
            var userslist = await _userRepository.GetAll<User>();
            return userslist;
        }

        public async Task<bool> AddAsync(User user)
        {
            if (user == null) return false;

            var newUser = await _userRepository.GetById<User>(user.UserId.ToString());
            if (newUser != null) return false;

            bool bResult = await _userRepository.AddAsync(user);
            _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> UpdateAsync(User updateUser)
        {
            if (updateUser == null) return false;
            
            var updateHaveUser = await _userRepository.GetById<User>(updateUser.UserId.ToString());
            if (updateHaveUser == null) return false;

            var newUser = new User();
            newUser.UserName = updateUser.UserName;
            newUser.UserId = updateUser.UserId;
            newUser.Sex = updateUser.Sex;
            newUser.Age = updateUser.Age;
            newUser.CityId = updateUser.CityId;
            newUser.CountryId = updateUser.CountryId;

            bool bResult = await _userRepository.UpdateAsync(newUser);
            _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> DeleteAsync(int UserId)
        {
            if (UserId <= 0) return false;
            
            var newUser = await _userRepository.GetById<User>(UserId.ToString());
            if (newUser == null) return false;
            
            bool bResult = await _userRepository.DeleteAsync(newUser);
            _msDBConn.Commit();
            return bResult;
        }
    }
}

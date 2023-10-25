using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;
using Generally;
using Generic.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;

namespace UnitOfWorkPJEx_DapperService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMsDBConn _msDBConn;

        public UserService(IUserRepository userRepository, IMsDBConn msDBConn)
        {
            _userRepository = userRepository;
            _msDBConn = msDBConn;
        }

        public async Task<User> GetById(int UserId)
        {
            if (UserId <= 0)
            {
                return null;
            }
            var User = await _userRepository.GetById<User>(UserId);
            return User;

        }

        public async Task<IEnumerable<User>> GetUserAll()
        {
            var userslist = await _userRepository.GetAll<User>();
            return userslist;
        }

        public async Task<bool> AddUser(User user)
        {
            if (user == null)
            {
                return false;
            }
            var newUser = await _userRepository.GetById<User>(user.UserId);
            if (newUser != null)
            {
                return false;
            }

            bool bResult = await _userRepository.Add(user);
            _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> UpdateUser(User updateUser)
        {
            if (updateUser == null)
            {
                return false;
            }
            var updateHaveUser = await _userRepository.GetById<User>(updateUser.UserId);
            if (updateHaveUser == null)
            {
                return false;
            }
            var newUser = new User();
            newUser.UserName = updateUser.UserName;
            newUser.UserId = updateUser.UserId;
            newUser.Sex = updateUser.Sex;
            newUser.Age = updateUser.Age;
            newUser.CityId = updateUser.CityId;
            newUser.CountryId = updateUser.CountryId;

            bool bResult = await _userRepository.Update(newUser);
            _msDBConn.Commit();
            return bResult;
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            if (UserId <= 0)
            {
                return false;
            }
            var newUser = await _userRepository.GetById<User>(UserId);
            if (newUser == null)
            {
                return false;
            }
            bool bResult = await _userRepository.Delete(UserId);
            _msDBConn.Commit();
            return bResult;
        }
    }
}

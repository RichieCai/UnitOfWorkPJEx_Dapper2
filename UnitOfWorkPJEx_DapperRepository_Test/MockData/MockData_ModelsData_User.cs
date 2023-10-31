using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using static UnitOfWorkPJEx_DapperRepository.Models.DataModels.User;

namespace UnitOfWorkPJEx_DapperRepository_Test.MockData
{
    public static class MockData_ModelsData_User
    {
        public static User GetUser(string sUserId)
        {
            return GetUserAll().Where(x => x.UserId == int.Parse(sUserId)).SingleOrDefault();
        }

        public static List<User> GetUserAll()
        {
            return new List<User>()
            {
                new User()
                { 
                    UserId=1,
                    UserName="sam",
                    Age=25,
                    Sex=eSex.male,
                    CountryId=1,
                    CityId="1001",
                },
                new User()
                {
                    UserId=2,
                    UserName="ken",
                    Age=32,
                     Sex=eSex.male,
                    CountryId=1,
                    CityId="1001",
                },
                new User()
                {
                    UserId=6,
                    UserName="danny",
                    Age=66,
                    Sex=eSex.male,
                    CountryId=3,
                    CityId="1003",
                },
                new User()
                {
                    UserId=7,
                    UserName="jokson",
                    Age=88,
                     Sex=eSex.male,
                    CountryId=3,
                    CityId="3003",
                },
                 new User()
                {
                    UserId=11,
                    UserName="amy",
                    Sex=eSex.female,
                    Age=36,
                    CountryId=2,
                    CityId="2003",
                },
                new User()
                {
                    UserId=14,
                    UserName="dora",
                    Sex=eSex.female,
                    Age=35,
                    CountryId=2,
                    CityId="2001",
                },
                new User()
                {
                    UserId=17,
                    UserName="tammy",
                    Sex=eSex.female,
                    Age=28,
                    CountryId=2,
                    CityId="1002",
                },
            };
        }

        public static User AddUser() 
        {
            return new User()
            {
                UserId = 18,
                UserName = "ladygaga",
                Age = 55,
                Sex = eSex.female,
                CountryId = 2,
                CityId = "2002",
            };
        }

        public static User UpdateUserResult()
        {
            return new User()
            {
                UserId = 2,
                UserName = "ladygaga",
                Age = 55,
                Sex = eSex.female,
                CountryId = 2,
                CityId = "2002",
            };
        }


    }
}

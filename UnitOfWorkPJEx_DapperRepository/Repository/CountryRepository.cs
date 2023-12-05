using Dapper;
using MyCommon.Interface;
using UnitOfWorkPJEx_DapperRepository.Interface;

namespace UnitOfWorkPJEx_DapperRepository.Repository
{
    public class CountryRepository: ICountryRepository
    {
        public IMsDBConn _conn;
        public CountryRepository(IMsDBConn conn)
        {
            _conn = conn;
        }

        public async Task<Country> Get<Country>(string CountryId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("CountryId", CountryId);
            string sCmd = @$" select * from [Country] where CountryId=@CountryId";

           return await _conn.QuerySingleAsync<Country>(sCmd, parameter);

        }

        public async Task<IEnumerable<Country>> GetAll<Country>()
        {
            string sCmd = @$" select * from [Country] ";
            var result = await _conn.QueryAsync<Country>(sCmd);
            return result;
        }

        public async Task<IEnumerable<CountryVM>> GetCountryCityAll<CountryVM>()
        {
            string sCmd = @$" 
            SELECT co.CountryId,co.CountryName,ci.CityId,ci.CityName
            FROM Country co
            left join  City ci on co.CountryId=ci.CountryId
            where co.Status=1 and ci.Status=1
            order by co.orderby,ci.orderby
            ";
            var result = await _conn.QueryAsync<CountryVM>(sCmd);
            return result;
        }
    }
}

namespace UnitOfWorkPJEx_DapperRepository.Interface
{
    public interface ICountryRepository
    {
        Task<Country> Get<Country>(string CountryId);
        Task<IEnumerable<Country>> GetAll<Country>();

        Task<IEnumerable<CountryVM>> GetCountryCityAll<CountryVM>();
    }
}

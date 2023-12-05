
//using Microsoft.AspNetCore.Builder;
using MyCommon;
using MyCommon.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Models.DataModels;
using UnitOfWorkPJEx_DapperRepository.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UnitOfWorkPJEx_DapperRepository
{
    public static class ServiceExtension
    {
        private readonly static string _DefConnectionName = "DefaultConnection";
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            //  services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //  services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();

            // services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper(connection));//給予參數的範例

            services.AddScoped<IMsDBConn>(provider => new MsDBConn(configuration.GetConnectionString(_DefConnectionName)));
            return services;
        }
    }
}

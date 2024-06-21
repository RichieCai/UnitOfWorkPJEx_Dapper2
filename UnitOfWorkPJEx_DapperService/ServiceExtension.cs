using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using System.Data;
using MyCommon.Interface;
using MyCommon;

namespace UnitOfWorkPJEx_DapperService
{
    public class ServiceExtension_Service
    {
        private readonly static string _DefConnectionName = "DefaultConnection";
        public static void AddDbContexts(WebApplicationBuilder? builder)
        {
            //builder.Services.AddSingleton<IConfiguration>(configuration);
            builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IUnitOfWork_Dapper, UnitOfWork_Dapper>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICountryRepository, CountryRepository>();

            //  builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //  builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            // builder.Services.AddScoped<IDatabaseHelper>(x => new DatabaseHelper(connection));//給予參數的範例
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        }
    }
}

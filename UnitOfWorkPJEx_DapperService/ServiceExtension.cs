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
        public static void AddDbContexts(WebApplicationBuilder? builder)
        {
            builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IUnitOfWork_Dapper, UnitOfWork_Dapper>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        }
    }
}

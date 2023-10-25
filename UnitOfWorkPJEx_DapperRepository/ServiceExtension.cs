
//using Microsoft.AspNetCore.Builder;
using Generally;
using Generic.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;

namespace UnitOfWorkPJEx_DapperRepository
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMsDBConn, MsDBConn>();

            return services;
        }
    }
}

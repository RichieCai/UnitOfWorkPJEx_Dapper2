
//using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCommon;
using MyCommon.Interface;

namespace UnitOfWorkPJEx_DapperRepository
{
    public class ServiceExtension_Repository
    {
        private readonly static string _DefConnectionName = "DefaultConnection";
        public static void AddDbContexts(WebApplicationBuilder? builder)
        {
            builder.Services.AddScoped<IMsDBConn>(provider => new MsDBConn(builder.Configuration.GetConnectionString(_DefConnectionName)));
        }
    }
}

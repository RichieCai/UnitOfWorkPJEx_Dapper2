using UnitOfWorkPJEx_DapperRepository;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperService.Service;
using  UnitOfWorkPJEx_DapperService;
using UnitOfWorkPJEx_Dapper2;
using UnitOfWorkPJEx_DapperRepository.Interface;
using UnitOfWorkPJEx_DapperRepository.Repository;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Logging.AddLog4Net("Configs/log4net.Config");

ServiceExtension_Service.AddDbContexts(builder);//將註冊地方更改為UnitOfWorkPJEx_DapperService專案;
ServiceExtension_Repository.AddDbContexts(builder);//將註冊地方更改為UnitOfWorkPJEx_DapperRepository專案;

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddScoped<IUserGenericService, UserGenericService>();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.IncludeXmlComments(string.Format(@"{0}\Dapper.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory));
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "WebApi for Dapper",
//    });
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Api Dapper test 1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

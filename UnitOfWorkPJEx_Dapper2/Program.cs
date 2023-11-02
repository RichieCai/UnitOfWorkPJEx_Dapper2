using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UnitOfWorkPJEx_DapperRepository;
using UnitOfWorkPJEx_DapperRepository.Context;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperService.Service;

var builder = WebApplication.CreateBuilder(args);

// AddAsync services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Logging.AddLog4Net("Configs/log4net.Config");



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

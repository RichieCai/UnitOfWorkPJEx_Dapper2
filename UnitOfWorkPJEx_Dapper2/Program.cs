using Microsoft.EntityFrameworkCore;
using UnitOfWorkPJEx_DapperRepository;
using UnitOfWorkPJEx_DapperRepository.Context;
using UnitOfWorkPJEx_DapperService.Interface;
using UnitOfWorkPJEx_DapperService.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<IUserService, UserService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;

using MXAppMedia.Api.Context;
using MXAppMedia.Api.Repositories;
using MXAppMedia.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var PostgreSqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(PostgreSqlConnection));

//AutoMapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Dependency Injection for Repositories
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

//Dependency Injection for Services
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IClientService, ClientService>();


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

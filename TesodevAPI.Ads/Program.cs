using Microsoft.AspNetCore.DataProtection.Repositories;
using TesodevAPI.Ads.Services;
using MongoDB.Driver;
using TesodevAPI.Ads;
using TesodevAPI.Ads.Models;
using TesodevAPI.Ads.Repositories;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
/*builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile("config/appsettings.Development.json", optional: false, reloadOnChange:true);
builder.Configuration.AddJsonFile("config/appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile("config/MongoDataBaseSettings.json");
builder.Configuration.AddEnvironmentVariables();*/
builder.Services.Configure<MongoDatabaseSettings>(builder.Configuration.GetSection("MongoDataBaseSettings"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IService,Service>();
builder.Services.AddSingleton<IRepository,Repository>();

var app  = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUi3();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


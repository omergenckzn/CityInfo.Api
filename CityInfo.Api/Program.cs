using CityInfo.Api;
using CityInfo.Api.DbContexts;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.
    Console().WriteTo.File("logs/cityinfo.txt", rollingInterval: 
    RollingInterval.Day).CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



//builder.Services.AddTransient<IMailService,LocalMailService>();
builder.Services.AddTransient<IMailService, CloudMailService>();


builder.Services.AddSingleton<CitiesDataStore>();

builder.Services.AddDbContext<CityInfoContext>
    (dbContextOptions => dbContextOptions.UseSqlite("Data Source=CityInfo.db"));

builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

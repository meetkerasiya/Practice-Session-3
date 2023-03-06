using Microsoft.EntityFrameworkCore;
using Students.API.Middleware;
using Students.API.Models;
using Students.API.Models.Repository;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

//For EF Core 
var services = builder.Services;
services.AddDbContext<StudentContext>(option =>
            option.UseSqlServer(builder.Configuration
            .GetConnectionString("StudentDB")));
services.AddScoped<IDataRepository<Student>,StudentRepository>();

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();   

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//serilog
var configuration=new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
Log.Logger =new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();


app.UseAuthorization();
app.UseMiddleware<RequestResponseLogMiddleware>();
app.UseMiddleware<TimeMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

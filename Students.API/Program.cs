using Microsoft.EntityFrameworkCore;
using Students.API.Middleware;
using Students.API.Models;
using Students.API.Models.Repository;

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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TimeMiddleware>();
app.MapControllers();

app.Run();

using FutureMountainAPI;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

string connectionString = "Server=localhost\\SQLEXPRESS;Database=FutureMountain;Trusted_Connection=True;";

//var test = System.Configuration.ConfigurationManager.ConnectionStrings["CubeDataDbContext"].ConnectionString;

//builder.Services.AddDbContext<CubeDataDbContext>(options => options.UseSqlServer(
//    System.Configuration.ConfigurationManager.ConnectionStrings["CubeDataDbContext"].ConnectionString));
builder.Services.AddDbContext<CubeDataDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<DateDbContext>(options => options.UseSqlServer(connectionString));

// Default Policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:90", "http://localhost:80")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();

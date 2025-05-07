using DotNetEnv;
using EmployeeManagement.API.Data;
using EmployeeManagement.API.Data.Repositories;
using EmployeeManagement.API.Services;

// Load environment variables from .env file
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Connection
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                      builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new DatabaseConnection(connectionString));

// Register repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("https://localhost:7001") // Frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();
app.MapControllers();

app.Run();
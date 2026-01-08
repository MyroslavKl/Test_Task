using EmployeeTask.API.Repositories.Interfaces;
using EmployeeTask.API.Repositories;
using EmployeeTask.API.Services.Interfaces;
using EmployeeTask.API.Services;
using EmployeeTask.API.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//For now I use built-in logger but for better loging configuration use Serilog
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();

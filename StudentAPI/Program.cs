using StudentAPI.Repositories;
using StudentAPI.ServicesLayer;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Repository Layer
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Service Layer
builder.Services.AddScoped<IStudentService, StudentService>();

// Add Memory Cache (needed for caching in controller)
builder.Services.AddMemoryCache();

// Swagger / API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
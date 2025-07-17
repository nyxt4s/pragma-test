using Microsoft.EntityFrameworkCore;
using pragma_api.Models;
using pragma_api.Repositories;
using pragma_api.Repositories.Interfaces;
using pragma_api.Services;
using pragma_api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PragmadbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")
                     ?? throw new Exception("missing connectionstring")));

builder.Services.AddControllers();

// scoped services
builder.Services.AddScoped<IUserService, UserService>();

// scoped repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",    
                "http://localhost:5173",    
                "http://localhost:3001",    
                "http://localhost:8080"     
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    
    // Política más permisiva para desarrollo
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

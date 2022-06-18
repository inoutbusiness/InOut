using InOut.Infrastructure.Context;
using InOut.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

Injector.InjectIoCServices(builder.Services);

builder.Services.AddDbContext<InOutContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("InOutDefaultConnection")), ServiceLifetime.Transient);

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

app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .WithOrigins("http://localhost:3000"));

app.UseAuthorization();

app.MapControllers();

app.Run();
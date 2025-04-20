using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseInMemoryDatabase("ProductTestDb");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandler>();

app.MapControllers();

app.Run();

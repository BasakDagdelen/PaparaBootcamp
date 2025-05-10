using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Extensions;
using Patikadev_RestfulApi.Interfaces;
using Patikadev_RestfulApi.Middleware;
using Patikadev_RestfulApi.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.ApplicationExtensions(builder.Configuration);    // extensions

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLogging();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

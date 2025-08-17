using Infrastructure.Configuration;
using Infrastructure.DataBase.Context;
using Lesson009_WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrustructure(builder.Configuration);

var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "botData.db");
var connectionString = $"Data Source={dbPath}";

Console.WriteLine(dbPath);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

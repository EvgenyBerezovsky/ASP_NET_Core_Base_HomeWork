using ASP_NET_Core_Base_HomeWork_Lesson005.Interfaces;
using ASP_NET_Core_Base_HomeWork_Lesson005.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddTransient<IBbookValidationService, BookValidationService>();
builder.Services.AddTransient<IBookFilterService, BookFilterService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
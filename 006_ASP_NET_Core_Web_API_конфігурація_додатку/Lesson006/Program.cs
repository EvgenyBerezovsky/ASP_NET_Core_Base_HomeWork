
using ASP_NET_Core_Base_HomeWork_Lesson006.Interfaces;
using ASP_NET_Core_Base_HomeWork_Lesson006.Services;
using Lesson006;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOptions<CastomConfigurationData>().BindConfiguration(nameof(CastomConfigurationData));

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IBookService, BookService>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IBookValidationService, BookValidationService>();

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

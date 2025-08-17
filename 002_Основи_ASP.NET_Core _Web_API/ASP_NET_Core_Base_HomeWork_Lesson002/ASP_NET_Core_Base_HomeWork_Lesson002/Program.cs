using static System.Net.WebRequestMethods;

internal class Program
{
    // Завдання:

    // Створити проєкт ASP.NET Core Web API.Додати в проєкт новий контролер. В новий контролер
    // додати декілька кінцевих точок, з типом HTTP запиту GET та прописати для кожної з них
    // різний шлях.Проекспериментувати з отриманням декількох параметрів кінцевої точки.Впевнитись,
    // що Swagger відобразить зроблені вами зміни і додасть нові кінцеві точки та новий контролер
    // для них у вебклієнті.

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddSwaggerGen();

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
    }
}
internal class Program
{

    //    Завдання:
    //    Створити три кінцеві точки з HTTP методом GET. Перша точка буде мати шлях «/addition» відносно адреси нашого застосунку
    //    і приймати два числа. При виклику кінцевої точки вона має повертати результат додавання цих двох чисел. Наприклад виклик
    //    кінцевої точки https://localhost:7089/addition/1&2 має повернути число 3. Друга точка буде мати шлях «/subtraction» і
    //    так само приймати два числа, але в результаті має віддавати різницю між першим числом та другим. Наприклад виклик
    //    кінцевої точки https://localhost:7089/subtraction/5&4 має повернути 1. Третя кінцева точка буде мати шлях «/division»
    //    і при її виклику перше число має ділитися на друге. Наприклад виклик кінцевої точки https://localhost:7089/division/2&4
    //    має повернути 0,5. Рекомендується спробувати викликати останню кінцеву точку зі значеннями 5 і 0. Ви отримаєте не
    //    стандартну відповідь від кінцевої точки, яку ми будемо розбирати на інших уроках.

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/addition/{input}", (string input) =>
        {
            var numbers = input.Split('&').Select(int.Parse).ToArray();
            if (numbers.Length != 2)
                return Results.BadRequest("Please provide two numbers separated by '&'.");
            var result = numbers[0] + numbers[1];
            return Results.Ok(result);
        });

        app.MapGet("/subtraction/{input}", (string input) =>
        {
            var numbers = input.Split('&').Select(int.Parse).ToArray();
            if (numbers.Length != 2)
                return Results.BadRequest("Please provide two numbers separated by '&'.");
            var result = numbers[0] - numbers[1];
            return Results.Ok(result);
        });

        app.MapGet("/division/{input}", (string input) =>
        {
            var numbers = input.Split('&').Select(int.Parse).ToArray();
            //if (numbers.Length != 2)
            //    return Results.BadRequest("Please provide two numbers separated by '&'.");
            //if (numbers[1] == 0)
            //    return Results.BadRequest("Division by zero is not allowed.");
            var result = (double)numbers[0] / numbers[1];
            return Results.Ok(result);
        });

        app.Run();
    }
}
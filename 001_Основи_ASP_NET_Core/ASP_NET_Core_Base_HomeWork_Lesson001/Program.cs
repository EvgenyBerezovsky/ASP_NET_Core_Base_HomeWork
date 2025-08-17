internal class Program
{

    //    ��������:
    //    �������� ��� ����� ����� � HTTP ������� GET. ����� ����� ���� ���� ���� �/addition� ������� ������ ������ ����������
    //    � �������� ��� �����. ��� ������� ������ ����� ���� �� ��������� ��������� ��������� ��� ���� �����. ��������� ������
    //    ������ ����� https://localhost:7089/addition/1&2 �� ��������� ����� 3. ����� ����� ���� ���� ���� �/subtraction� �
    //    ��� ���� �������� ��� �����, ��� � ��������� �� �������� ������ �� ������ ������ �� ������. ��������� ������
    //    ������ ����� https://localhost:7089/subtraction/5&4 �� ��������� 1. ����� ������ ����� ���� ���� ���� �/division�
    //    � ��� �� ������� ����� ����� �� ������� �� �����. ��������� ������ ������ ����� https://localhost:7089/division/2&4
    //    �� ��������� 0,5. ������������� ���������� ��������� ������� ������ ����� � ���������� 5 � 0. �� �������� ��
    //    ���������� ������� �� ������ �����, ��� �� ������ ��������� �� ����� ������.

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
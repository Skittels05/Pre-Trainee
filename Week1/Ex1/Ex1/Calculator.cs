using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    internal class Calculator
    {
        private readonly Dictionary<char, ICalculate> operations = new Dictionary<char, ICalculate>
        {
            { '+', new Addition() },
            { '-', new Subtraction() },
            { '*', new Multiplication() },
            { '/', new Division() }
        };
        public void Run()
        {
            while (true)
            {
                try
                {
                    double num1 = GetNumber("Введите первое число: ");
                    char op = GetOperation();
                    double num2 = GetNumber("Введите второе число: ");

                    ICalculate operation = operations[op];
                    double result = operation.Execute(num1, num2);
                    Console.WriteLine($"Результат: {num1} {op} {num2} = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                if (!AskToContinue()) break;
            }
        }

        private double GetNumber(string request)
        {
            Console.Write(request);
            if (!double.TryParse(Console.ReadLine(), out double number))
            {
                Console.WriteLine("Некорректный формат!");
                return GetNumber(request);
            }
            return number;
        }

        private char GetOperation()
        {
            Console.Write("Введите операцию (+, -, *, /): ");
            string input = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(input) || input.Length != 1 || !operations.ContainsKey(char.Parse(input)))
            {
                Console.WriteLine("Некорректная операция! Допустимые операции: +, -, *, /");
                return GetOperation();
            }
            return input[0];
        }

        private bool AskToContinue()
        {
            Console.Write("Хотите продолжить? (да/нет): ");
            string response = Console.ReadLine().Trim().ToLower();
            if (response == "да")
            {
                return true;
            }
            else if (response == "нет")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Некорректный ввод! Введите 'да' или 'нет'.");
                return AskToContinue();
            }
        }

    }
}

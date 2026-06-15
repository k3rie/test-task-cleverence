using System;
using System.Text;

class Program
{
    static void Main()
    {    

        Console.WriteLine("1 - Сжатие");
        Console.WriteLine("2 - Распаковка");
        Console.Write("Выберите действие: ");

        string choice = Console.ReadLine();

        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine("Результат: " + Compress(input));
        }
        else if (choice == "2")
        {
            Console.WriteLine("Результат: " + Decompress(input));
        }
        else
        {
            Console.WriteLine("Неверный выбор.");
        }
    }

    static string Compress(string s)
    {
        if (string.IsNullOrEmpty(s))
            return s;

        StringBuilder result = new StringBuilder();

        int count = 1;

        for (int i = 1; i <= s.Length; i++)
        {
            if (i < s.Length && s[i] == s[i - 1])
            {
                count++;
            }
            else
            {
                result.Append(s[i - 1]);

                if (count > 1)
                    result.Append(count);

                count = 1;
            }
        }

        return result.ToString();
    }

    static string Decompress(string s)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < s.Length; i++)
        {
            char current = s[i];

            if (char.IsLetter(current))
            {
                string number = "";

                while (i + 1 < s.Length && char.IsDigit(s[i + 1]))
                {
                    number += s[i + 1];
                    i++;
                }

                int count = number == "" ? 1 : int.Parse(number);

                for (int j = 0; j < count; j++)
                {
                    result.Append(current);
                }
            }
        }

        return result.ToString();
    }
}
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("input.txt");

        using StreamWriter output = new StreamWriter("output.txt");
        using StreamWriter problems = new StreamWriter("problems.txt");

        foreach (string line in lines)
        {
            LogEntry entry = ParseLine(line);

            if (entry == null)
            {
                problems.WriteLine(line);
                continue;
            }

            output.WriteLine(
                $"{entry.Date}\t{entry.Time}\t{entry.Level}\t{entry.Method}\t{entry.Message}");
        }

        Console.WriteLine("Обработка завершена.");
    }

    static LogEntry ParseLine(string line)
    {
        // Формат 1
        Match m1 = Regex.Match(
            line,
            @"^(\d{2}\.\d{2}\.\d{4})\s+([\d:.]+)\s+(\w+)\s+(.*)$");

        if (m1.Success)
        {
            DateTime date = DateTime.ParseExact(
                m1.Groups[1].Value,
                "dd.MM.yyyy",
                CultureInfo.InvariantCulture);

            return new LogEntry
            {
                Date = date.ToString("yyyy-MM-dd"),
                Time = m1.Groups[2].Value,
                Level = NormalizeLevel(m1.Groups[3].Value),
                Method = "DEFAULT",
                Message = m1.Groups[4].Value
            };
        }

        // Формат 2
        Match m2 = Regex.Match(
            line,
            @"^(\d{4}-\d{2}-\d{2})\s+([\d:.]+)\|\s*(\w+)\|\d+\|([^|]+)\|\s*(.*)$");

        if (m2.Success)
        {
            return new LogEntry
            {
                Date = m2.Groups[1].Value,
                Time = m2.Groups[2].Value,
                Level = NormalizeLevel(m2.Groups[3].Value),
                Method = m2.Groups[4].Value,
                Message = m2.Groups[5].Value
            };
        }

        return null;
    }

    static string NormalizeLevel(string level)
    {
        switch (level.ToUpper())
        {
            case "INFORMATION":
                return "INFO";

            case "WARNING":
                return "WARN";

            default:
                return level.ToUpper();
        }
    }
}

class LogEntry
{
    public string Date { get; set; }
    public string Time { get; set; }
    public string Level { get; set; }
    public string Method { get; set; }
    public string Message { get; set; }
}
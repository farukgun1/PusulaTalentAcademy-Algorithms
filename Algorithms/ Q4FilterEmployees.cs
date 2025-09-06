using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public static class Q4FilterEmployees
{
    public static string FilterEmployees(
        IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
        {
            return JsonSerializer.Serialize(new {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }


        var hireCutoffInclusive = new DateTime(2017, 12, 31);

        var filtered = employees
            .Where(e => e.Age >= 25 && e.Age <= 40) 
            .Where(e => string.Equals(e.Department, "IT", StringComparison.OrdinalIgnoreCase)
                     || string.Equals(e.Department, "Finance", StringComparison.OrdinalIgnoreCase))
            .Where(e => e.Salary >= 5000m && e.Salary <= 9000m) 
            .Where(e => e.HireDate >= hireCutoffInclusive)      
            .ToList();

        var names = filtered
            .Select(e => e.Name)
            .OrderByDescending(n => n?.Length ?? 0)
            .ThenBy(n => n, StringComparer.Ordinal)   
            .ToArray();

        int count = filtered.Count;
        decimal total = count > 0 ? filtered.Sum(e => e.Salary)   : 0m;
        decimal avg   = count > 0 ? filtered.Average(e => e.Salary) : 0m;
        decimal min   = count > 0 ? filtered.Min(e => e.Salary)   : 0m;
        decimal max   = count > 0 ? filtered.Max(e => e.Salary)   : 0m;

        avg = Math.Round(avg, 2, MidpointRounding.AwayFromZero);

        var result = new {
            Names = names,
            TotalSalary = total,
            AverageSalary = avg,
            MinSalary = min,
            MaxSalary = max,
            Count = count
        };

        return JsonSerializer.Serialize(result);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace Algorithms
{
    public static class Q3FilterPeopleFromXml
    {
        public static string FilterPeopleFromXml(string xmlData)
        {
            if (string.IsNullOrWhiteSpace(xmlData))
                return JsonSerializer.Serialize(new
                {
                    Names = Array.Empty<string>(),
                    TotalSalary = 0,
                    AverageSalary = 0,
                    MaxSalary = 0,
                    Count = 0
                });

            var doc = XDocument.Parse(xmlData);

            var people = doc.Descendants("Person")
                .Select(p => new
                {
                    Name = (p.Element("Name")?.Value ?? "").Trim(),
                    Age = int.TryParse((p.Element("Age")?.Value ?? "").Trim(), out var a) ? a : 0,
                    Department = (p.Element("Department")?.Value ?? "").Trim(),
                    Salary = decimal.TryParse((p.Element("Salary")?.Value ?? "").Trim(),
                               NumberStyles.Number, CultureInfo.InvariantCulture, out var s) ? s : 0,
                    HireDate = DateTime.TryParse((p.Element("HireDate")?.Value ?? "").Trim(),
                               out var d) ? d : DateTime.MaxValue
                })
                .Where(x => x.Age > 30)
                .Where(x => string.Equals(x.Department, "IT", StringComparison.OrdinalIgnoreCase))
                .Where(x => x.Salary > 5000)
                .Where(x => x.HireDate < new DateTime(2019, 1, 1))
                .ToList();

            var names = people.Select(x => x.Name).OrderBy(n => n).ToArray();
            int count = people.Count;
            decimal total = count > 0 ? people.Sum(x => x.Salary) : 0;
            decimal avg   = count > 0 ? people.Average(x => x.Salary) : 0;
            decimal max   = count > 0 ? people.Max(x => x.Salary) : 0;

            var result = new
            {
                Names = names,
                TotalSalary = total,
                AverageSalary = avg,
                MaxSalary = max,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}

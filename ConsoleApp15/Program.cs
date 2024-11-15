using System;
using System.Collections.Generic;
using System.Linq;

class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int DepId { get; set; }
}

class Department
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}

class Program
{
    static void Main()
    {
        List<Department> departments = new List<Department>()
        {
            new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
            new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
            new Department(){ Id = 3, Country = "France", City = "Paris" },
            new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
        };

        List<Employee> employees = new List<Employee>()
        {
            new Employee() { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
            new Employee() { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
            new Employee() { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
            new Employee() { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
            new Employee() { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
            new Employee() { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2 },
            new Employee() { Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4 }
        };

        var ukrainian = employees
            .Join(departments, e => e.DepId, d => d.Id, (e, d) => new { e.FirstName, e.LastName, d.Country, d.City })
            .Where(x => x.Country == "Ukraine" && x.City != "Odesa")
            .Select(x => new { x.FirstName, x.LastName });

        Console.WriteLine("Employees working in Ukraine but not in Odesa:");
        foreach (var emp in ukrainian)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}");
        }

        var countries = departments
            .Select(d => d.Country)
            .Distinct();

        Console.WriteLine("\nList of unique countries:");
        foreach (var country in countries)
        {
            Console.WriteLine(country);
        }

        var emplTop = employees
            .Where(e => e.Age > 25)
            .Take(3);

        Console.WriteLine("\nFirst 3 employees older than 25:");
        foreach (var emp in emplTop)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Age: {emp.Age}");
        }

        var EmplKiev = employees
            .Join(departments, e => e.DepId, d => d.Id, (e, d) => new { e.FirstName, e.LastName, e.Age, d.City })
            .Where(x => x.City == "Kyiv" && x.Age > 23)
            .Select(x => new { x.FirstName, x.LastName, x.Age });

        Console.WriteLine("\nEmployees from Kyiv older than 23:");
        foreach (var emp in EmplKiev)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Age: {emp.Age}");
        }
    }
}

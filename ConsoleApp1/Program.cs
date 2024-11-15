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

        var ukrainian = from e in employees
                                 join d in departments on e.DepId equals d.Id
                                 where d.Country == "Ukraine" && d.City != "Odesa"
                                 select new { e.FirstName, e.LastName };

        Console.WriteLine("Employees working in Ukraine but not in Odesa:");
        foreach (var emp in ukrainian)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}");
        }

        var countries = (from d in departments
                         select d.Country).Distinct();

        Console.WriteLine("\nList of unique countries:");
        foreach (var country in countries)
        {
            Console.WriteLine(country);
        }

        var emplTop = (from e in employees
                            where e.Age > 25
                            select e).Take(3);

        Console.WriteLine("\nFirst 3 employees older than 25:");
        foreach (var emp in emplTop)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Age: {emp.Age}");
        }

        var EmplKiev = from e in employees
                            join d in departments on e.DepId equals d.Id
                            where d.City == "Kyiv" && e.Age > 23
                            select new { e.FirstName, e.LastName, e.Age };

        Console.WriteLine("\nEmployees from Kyiv older than 23:");
        foreach (var emp in EmplKiev)
        {
            Console.WriteLine($"{emp.FirstName} {emp.LastName}, Age: {emp.Age}");
        }
    }
}

using System;
using System.Data.Entity;

namespace WebApiLayer.Models
{
    public class Employee
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public long ContactNumber { get; set; }

        public string Address { get; set; }
    }

    public class EmpDBContext : DbContext
    {
        public EmpDBContext()
        { }
        public DbSet<Employee> EmployeeModels { get; set; }
    }
}

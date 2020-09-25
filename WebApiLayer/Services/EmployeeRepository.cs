using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApiLayer.Models;

namespace Gateway.Services
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private EmpDBContext _db;

        public EmployeeRepository()
        {
            _db = new EmpDBContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>();
            });
        }

        public IEnumerable<Employee> GetAll()
        {
            return _db.EmployeeModels;
        }

        public Employee Get(int Id)
        {
            Employee employee = _db.EmployeeModels.SingleOrDefault(e => e.ID == Id);
            return employee;
        }

        public bool Create(Employee employee)
        {
            _db.EmployeeModels.Add(employee);

            _db.SaveChanges();

            return true;
        }

        public bool Edit(Employee employee)
        {
            Employee employeeRecord = _db.EmployeeModels.SingleOrDefault(e => e.ID == employee.ID);
            employeeRecord.Name = employee.Name;
            employeeRecord.ContactNumber = employee.ContactNumber;
            employeeRecord.Address = employee.Address;

            _db.SaveChanges();

            return true;
        }

        public void Delete(int Id)
        {
            _db.EmployeeModels.Remove(Get(Id));
            _db.SaveChanges();
        }

        public List<Employee> GetEmployeeByPage(int pageStart, int pageSize)
        {
            return _db.Database.SqlQuery<Employee>("UpdatedGetEmployees @MinRow, @MaxRow",
                new SqlParameter("MinRow", pageStart+1),
                new SqlParameter("MaxRow", pageStart+pageSize)).ToList();
        }

        public int GetEmployeeCount()
        {
            var countResult = _db.Database.SqlQuery<int>("GetCount").ToList();
            return countResult.First();
        }

    }
}

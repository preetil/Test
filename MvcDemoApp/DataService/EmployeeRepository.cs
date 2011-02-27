using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemoApp.DataService
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private EmployeeEntities employeeEntities = new EmployeeEntities();

        public IQueryable<Employee> GetAllEmployees()
        {
            return employeeEntities.Employees;
        }

        public Employee GetEmployee(string ssn)
        {
            return employeeEntities.Employees.Where(x => x.Ssn == ssn).FirstOrDefault();
        }

        public void Add(Employee employee)
        {
            employeeEntities.AddObject("Employees", employee);

        }

        public void Delete(Employee employee)
        {
            employeeEntities.DeleteObject(employee);
        }

        public void Save()
        {
            employeeEntities.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public class SQLEmployeeRepository : IEmployeeREpository
    {
        private readonly AppDBContext _context;

        public SQLEmployeeRepository(AppDBContext context)
        {
            _context = context;      
        }
        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _context.Employees.Find(id);
            if (employee!=null)
            {
                _context.Employees.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GtEmployee(int id)
        {
            return _context.Employees.Find(id);
        }

        public Employee Upate(Employee employee)
        {
            //important
           var  OldEmployee= _context.Employees.Attach(employee);
            OldEmployee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return employee;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public class MOKEmployeeRepository : IEmployeeREpository
    {
        private readonly List<Employee> _EmployeeList;

        public MOKEmployeeRepository()
        {
            _EmployeeList = new List<Employee>()
            {
                new Employee(){Id=1,Name="ahmed abdo",Department=Dep.it,Email="ahmed@.com" },
                new Employee(){Id=2,Name="asmaa 3rafa",Department=Dep.@is,Email="asmaa@.com" },
                new Employee(){Id=3,Name="shrouk",Department=Dep.cs,Email="shrouk@.com" }
            };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _EmployeeList.Max(e => e.Id)+1;
            _EmployeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _EmployeeList.FirstOrDefault(e => e.Id == id);
            if (employee!=null)
            {
                _EmployeeList.Remove(employee);

            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _EmployeeList;
        }

        public Employee GtEmployee(int id)
        {
            return _EmployeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee Upate(Employee employee)
        {
            Employee OldEmployee = _EmployeeList.FirstOrDefault(e => e.Id == employee.Id);
            if (OldEmployee!=null)
            {
                OldEmployee.Email = employee.Email;
                OldEmployee.Department = employee.Department;
                OldEmployee.Name = employee.Name;
            }
            return OldEmployee;
        }
    }
}

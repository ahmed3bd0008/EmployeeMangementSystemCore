using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
     public interface IEmployeeREpository
    {
        Employee GtEmployee(int id);
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Delete(int id);
        Employee Upate(Employee employee);

    }
}

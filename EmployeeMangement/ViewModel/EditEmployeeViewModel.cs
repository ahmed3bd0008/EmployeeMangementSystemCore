using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{
    public class EditEmployeeViewModel:CreateEmployeViewModel
    {
        public int Id { get; set; }
        public string ExtistinPhoto { get; set; }
    }
}

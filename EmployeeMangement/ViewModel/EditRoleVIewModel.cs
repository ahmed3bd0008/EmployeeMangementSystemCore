using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{

    public class EditRoleVIewModel
    {
        public EditRoleVIewModel()
        {
            Users = new List<string>();
        }
        public string ID { get; set; }
        public string RoleName { get; set; }
        public List<string> Users { get; set; }
    }
}

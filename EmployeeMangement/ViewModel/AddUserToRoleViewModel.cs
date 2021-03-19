using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{
    public class AddUserToRoleViewModel
    {
        public String UserId { get; set; }
        public String RoleId { get; set; }
        public String RoleName { get; set; }
        public String UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{
    public class EditUserViewModel
    {

        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        [Required]
        [EmailAddress]
       // [Remote(action: "IsEmailInUsed", controller: "Account")]
       // [ValidEmailDomain(AllowEmail: "ahmed.com", ErrorMessage = "email must end with ahmed.com")]
        public String Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public List<string> Roles { get; set; }
        public string Id { get; set; }
    }
}

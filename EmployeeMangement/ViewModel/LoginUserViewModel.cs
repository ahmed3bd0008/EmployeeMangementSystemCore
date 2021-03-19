using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Remmber me")]
        public bool RemmberMe { get; set; }
    }
}

using EmployeeMangement.Untilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUsed",controller:"Account")]
        [ValidEmailDomain(AllowEmail:"ahmed.com",ErrorMessage ="email must end with ahmed.com")]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="not match to pasword")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
    }
}

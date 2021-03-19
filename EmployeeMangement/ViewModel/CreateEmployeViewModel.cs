using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using EmployeeMangement.Models;
using System.Collections.Generic;

namespace EmployeeMangement.ViewModel
{
    public class CreateEmployeViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "oveload lengh")]
        public string Name { get; set; }
        //department type is dep enum that required by default
        public Dep Department { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invaild email")]
        //to changedisplay  name from Email to Ofice Email
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        //هو بيعرض نفس اسم الproperities 

        public List< IFormFile> Photes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Models
{
    public  enum Dep
    {
        none,
        it,
        @is,
        cs
    }
    public class Employee
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="oveload lengh")]
        public string Name { get; set; }
        //department type is dep enum that required by default
        public Dep Department { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invaild email")]
        //to changedisplay  name from Email to Ofice Email
        [Display (Name="Office Email")]
        public string Email { get; set; }
        //هو بيعرض نفس اسم الproperities 

        public string PhotoPath { get; set; }

    }
}

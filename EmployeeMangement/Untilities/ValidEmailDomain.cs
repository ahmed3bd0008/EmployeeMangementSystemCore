using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangement.Untilities
{
    public class ValidEmailDomain:ValidationAttribute
    {
        readonly private string _AllowEmail;

        public ValidEmailDomain(string AllowEmail)
        {
            _AllowEmail = AllowEmail;
        }
        public override bool IsValid(object value)
        {
            string[] arrstr = value.ToString().Split("@");
            return arrstr[1].ToUpper() == _AllowEmail.ToUpper();
        }
    }
}

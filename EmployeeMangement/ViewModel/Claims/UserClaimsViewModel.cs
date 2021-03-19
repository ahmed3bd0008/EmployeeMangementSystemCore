using System.Collections.Generic;

namespace EmployeeMangement.ViewModel.Claims
{
    public class UserClaimsViewModel
    {
        //not from database i want to add to it
        public UserClaimsViewModel()
        {
            UserClaims = new List<CustomerClaims>();
        }
        public string UserId { get; set; }
        public List<CustomerClaims> UserClaims { get; set; }
    }
}

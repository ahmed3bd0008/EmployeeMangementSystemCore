using System.Collections.Generic;
using System.Security.Claims;

namespace EmployeeMangement.ViewModel.Claims
{
    public static class ConfigClaims
    {
        public static List<Claim>AllClaims()
        {
            List<Claim> claims = new List<Claim>()
           {
              new Claim("EditRole","EditRole"),
              new Claim("AddRole","AddRole"),
              new Claim("RemovRole","RemovRole")
           };
            return claims;
        }

    }
}

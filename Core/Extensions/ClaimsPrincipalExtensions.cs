using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions // bir kişinin claimlerini ararken .net biraz uğraştırır. JWT ile gelen clamiler okumak için claimsprincipal(claimlere erişmek için .net in kendi class ı) extend ettik. ilk metod claim type ı parametre olarak alır ve sonucu claim type a göre döndürürken ikinci metodda direk claimsPrincipal.ClaimRoles() ile direk rolleri dönüyoruz.
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}

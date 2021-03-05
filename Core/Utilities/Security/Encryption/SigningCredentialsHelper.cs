using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper //burada da asp.net web api ye gelen token ı doğrulayabilmesi için hangi anahtarı ve hangi algoritmayı kullanması gerektiğini belirtiyoruz. Başka bir deyişle bu sistemde bir JWT sistemi kullanılacak ve bu sistemde security key in bu kullanılan algoritma da budur diye bilgi veriyoruz.
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
        }
    }
}

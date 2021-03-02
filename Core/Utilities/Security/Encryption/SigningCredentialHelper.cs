using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialHelper
    {
        // bu class bir JWT sistemini yönetecek. Bu yüzden ona Anahtarını ve şifreleme algoritmasını söylüyoruz
        // Hashing işlemi yapıyor. Anahtar olarak securityKey, şifreleme olarak da SecurityAlgorithms'dan HmacSha512Signature kullanıyor. Hangi anahtar hangi doğrulamayı kullanacağını veriyoruz
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);   
        }
    }
}

using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        // kullanıcı arayüzden kullanıcı adı ve parolayı gönderdikten sonra CreateToken çalışacak. ilgili kullanıcı için veritabanına gidecek.
        // veritabanında bu kullanıcının claimlerini buluşturacak. orada bu bilgileri barıdıran bir tane jwt üretecek ve tekrar geri arayüze gönderecek
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}

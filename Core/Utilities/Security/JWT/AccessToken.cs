using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    // AccessToken anlamsız karakterlerden oluşan anahtar değeridir. o yüzden token'ı string olarak tutuyoruz. bir de bu token'ın geçerlilik süresini veriyoruz
    public class AccessToken
    {   
        public string Token { get; set; }               //anahtar
        public DateTime Expiration { get; set; }        //bitiş
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{   // bu class hash oluşturmaya ve onu doğrulamaya yarıyor. Hash oluştururken hangi algoritmayı kullanacağımızı söylüyoruz. Doğrularken de aynı algoritmayı ve daha önce oluşturduğumuz tuzu kullanarak onu doğruluyoruz
    public class HashingHelper
    {
        // burası verdiğimiz bir password değerinin hash ve salt değerlerini oluşturulmasına yarıyor
        public static void CreatePasswordHash(string password, out byte[] passworHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())                
            {
                // salt olarak kullandığımız algoritmanın(HMACSHA512()) .Key değerini kullanıyoruz  // algoritma Key değerini o an oluşturur // salt parolanın üzerine ilavelerle güçlendirilmesi işlemidir. parola tuzlaması
                passwordSalt = hmac.Key;
                passworHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        // sonradan sisteme girmek isteyen kişinin verdiği password'ün bizim veri kaynağımızdaki hash ile ilgili salt'a göre eşleşip eşleşmediğini kontrol ediyor, doğrulama yapıyor
        public static bool VerifyPasswordHash(string password, byte[] passworHash, byte[] passwordSalt)     // burada out olmamalı çünkü bu passwordHash ve passwordSalt değerlerini biz vereceğiz
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))                    // HMACSHA512 sınıfına Salt'ı da kullanmasını söylüyoruz. Yani anahtar vermiş oluyoruz
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passworHash[i])                                                  // hesaplanan Hash'in i'inci değeri ile veritabanındaki Hash'in i'inci değeri aynı değilse
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}

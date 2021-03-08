using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;             //burayı elle eklememiz gerekti. v.s. hatası
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception           // cacheAspect bir attribute
    {
        private int _duration;                              
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)                                           // constructer'da duration olarak tanımladığımız şey: eğer süre verilmezse, bu veri bellekte 60 dk kadar kalacak sonrasında cache'den / bellekten atacak
        {
            _duration = duration;                                                       // duration'ın set edilmesi
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }



        // çalışacak metodun namespace'i, ismi ve parametrelerine göre key oluşturuluyor. eğer bu key daha önce varsa cache'den al, yoksa veritabanından al ama cache'e de ekle
        public override void Intercept(IInvocation invocation)              // invocation methodumuz. mesela getall'ı çalıştırmadan önce bu kısmı çalıştırıyoruz. Intercept interception sınıfımızda bir metot. bunun yerine onbefore metodunu da kullanabilirdik
        {
                                              //Bu kısım namespace+class ismini verir// -metotun ismini belirtir
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");         // önce metedon ismini buluyoruz. invocation = metot, reflectedType = namespace'ini al. FullName = örneğin productmanager sınıfında varis alınan " :ProductService"'i de dahil et demek. Bu satır bir reflection'dır
                                             //yani: NorthWind.Business.IProductService.GetAll

            var arguments = invocation.Arguments.ToList();      // metodun parametrelerini listeye çevir
                                                                                                                    // bu kısım bir linq operasyonu
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";        // metotun parametrelerini tek tek (eğer parametre varsa) burada getall'ın içerisine ekle ( //yani: NorthWind.Business.IProductService.GetAll(parametre varsa burada belirtilir yoksa null)) // key'i böylece oluşturduk
                                                                                                                    // ?? : varsa soldakini ekle, yoksa sağdakini ekle demek. argument.Select liste döndürür yani parametreleri listeye çevirir. string.join de onları yanyana getirir
            if (_cacheManager.IsAdd(key))       // böyle bir cache key'i daha önceden var mı?
            {                            // varsa parantezin içini kullanacak
                invocation.ReturnValue = _cacheManager.Get(key);                 //invocation.ReturnValue : bu kısım metodu hiç çalıştırmadan geri dön demek. kendi kendine return oluştur demek oluyor (cache'deki keyi get et)
                return;
            }                           // yoksa
            invocation.Proceed();                                           // Proceed : invocation'ı devam ettir çalıştır demek (çalışınca veritabanından veriyi getirdi) 
            _cacheManager.Add(key, invocation.ReturnValue, _duration);      // veri gelince cache'e eklenenir
        }
    }
}
// özet olarak // çalışacak metodun namespace'i, ismi ve parametrelerine göre key oluşturuluyor. eğer bu key daha önce varsa cache'den al, yoksa veritabanından getir ve cache'e ekle

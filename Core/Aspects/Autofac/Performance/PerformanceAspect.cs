using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    // burası performans kontrolü sağlar yavaşlığı önleme amaçlıdır. bunu eğer ki core'da metot interceptor'da uygularsak bütün metotlarımızda uygulamış oluruz. (core utulities interceptors aspectinterceptorSelector)
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval)      // interval dediğimiz şey bir değer veriyoruz örneğin 5. bu işlem 5 saniyeyi geçerse beni uyar demek
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)            // metodun önünde kronometreyi başlatıyor
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)             // metot bittiğinde de o ana kadar geçen süreyi hesaplıyor
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)        // geçen süre intervalden (5 den) büyük ise
            {
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");      // burada log alma yöntemi kullanılmış ama maile vs yönlendirilebilir
            }
            _stopwatch.Reset();
        }
    }
}

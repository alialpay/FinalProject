using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));      //engin hocanın projede bu satırda log işlemi var. daha sonra uygulanabilir
            //classAttributes.Add(new ExceptionPerformanceAspect?(typeof(?)));      //[PerformanceAspect(5)] performansı tüm metotlarda kontrol etmek istersek ekleyebiliriz. 

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }

}

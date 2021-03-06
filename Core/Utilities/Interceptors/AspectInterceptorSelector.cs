using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            // yukarıdaki gibi classAttributes.Add(new PerformanceAspect) gibi bir kod ile mevcutta olan ve sonradan yazılacak tüm metodlarda performans aspectini devreye alabiliriz. Bu konum öyle bir nokta.
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
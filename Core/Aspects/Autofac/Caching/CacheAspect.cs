using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //reflection
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key,invocation.ReturnValue,_duration);
        }
    }
}
//ReflectedType name space demek + FullName dersen namespace + manager'ının (yani class ın)adı demek.
//Arguments = metodun (varsa) parametreleri demek
//invocation.returnvalue demek metodu çalıştırmadan cache managerdan getir demek (tabi cache de varsa kontrolü yapıldıktan sonra)
//cache de yoksa veritabanından ilgili veriyi getir ve cache de kaydet
//public override void Intercept(IInvocation invocation) metodu aslında namespace+class adı+metod adı+varsa metod parametreleri şeklinde bir key değeri oluşturuyor.Sonrasında aynı key e sahip değer cache de varsa direk oradan getir, yoksa veritabanından getir ve cache de ekle işini yapıyor tam olarak.

//var key = $"{methodName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})"; null değilse ve stringe çevrilebiliyorsa bu kısmı ekle ?? yoksa bunu ekle demek. arguments.select parametreleri seçer ve bir liste oluşturur, string.Join ise parametreleri virgülle ayırarak bir listeye dönüştürür.

using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    //Authorization Acpect kullanabilmek için oluşturduğumuz bir class. Authorization aspectleri genellikle business içinde yer alır. Çünkü her projenin yetkilendirme algoritması değişebilir. Altyapıyı Core içine yazıyoruz ama aspect kısmını genellikle Business içine yerleştiriyoruz.
    public class SecuredOperation:MethodInterception
    {
        //Bu aspect JWT için yazıldı.
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor =  ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>(); //JWT zincirin içinde olduğu için injection ile IConfiguration ı enjekte edip çalıştırabilmiştik. Ancak Aspect zincirin dışında olduğu için, bu satırla Autofac ile oluşturduğumuz servis mimarisine ulaş ve karşılığı al demiş oluyor. Örneğin elimizde productService olsa ve bir winform uygulası yazıyor olsak (dependency injection imkanımız olmasa) productService = ServiceTool.ServiceProvider.GerService<IProdductService>(); gibi IoC deki karşılığı alabilir ve kullanabiliriz.

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}

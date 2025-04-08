using Castle.DynamicProxy;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Business.Aspects
{
    public class BlacklistIpAspect : MethodInterception
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly List<string> _blacklistedIps;

        public BlacklistIpAspect(string blacklistedIps)
        {
            _blacklistedIps = blacklistedIps.Split(',').ToList();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) || _blacklistedIps.Contains(ipAddress))
            {
                throw new BadRequestException("Bu IP adresi kara listede, erişim izni yok.");
            }
        }
    }
}

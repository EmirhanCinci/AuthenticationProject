using Castle.DynamicProxy;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.Utilities.Interceptors;
using Infrastructure.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Business.Aspects
{
    public class MaintenanceModeAspect : MethodInterception
    {
        private readonly IConfiguration _configuration;

        public MaintenanceModeAspect()
        {
            _configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var isMaintenanceMode = bool.Parse(_configuration["MaintenanceMode"] ?? "false");
            if (isMaintenanceMode)
            {
                throw new BadRequestException("Sistem bakımda. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}

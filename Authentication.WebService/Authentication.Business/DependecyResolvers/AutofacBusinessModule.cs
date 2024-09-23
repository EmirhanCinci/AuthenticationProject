using Authentication.Business.BusinessRules;
using Authentication.Business.Implementations;
using Authentication.Business.Interfaces;
using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories;
using Authentication.DataAccess.Interfaces;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Utilities.Email.Implementations;
using Infrastructure.Utilities.Email.Interfaces;
using Infrastructure.Utilities.Files;
using Infrastructure.Utilities.Interceptors;
using System.Reflection;

namespace Authentication.Business.DependecyResolvers
{
    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfPasswordHistoryRepository>().As<IPasswordHistoryRepository>();
            builder.RegisterType<EfResetPasswordRequestRepository>().As<IResetPasswordRequestRepository>();
            builder.RegisterType<EfUserRefreshTokenRepository>().As<IUserRefreshTokenRepository>();
            builder.RegisterType<EfUserRepository>().As<IUserRepository>();
            builder.RegisterType<EfRoleRepository>().As<IRoleRepository>();
            builder.RegisterType<EfUserRoleRepository>().As<IUserRoleRepository>();

            builder.RegisterType<AuthenticationBusinessRules>();
            builder.RegisterType<UserBusinessRules>();
            builder.RegisterType<RoleBusinessRules>();
            builder.RegisterType<UserRoleBusinessRules>();
            builder.RegisterType<FileHelper>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<UserRoleService>().As<IUserRoleService>();

            builder.RegisterType<EmailService>().As<IEmailService>();

            builder.RegisterType<UnitOfWork<AuthenticationDbContext>>().As<IUnitOfWork<AuthenticationDbContext>>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions() { Selector = new AspectInterceptorSelector() }).SingleInstance();
        }
    }
}

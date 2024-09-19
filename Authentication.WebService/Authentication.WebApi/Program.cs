using Authentication.Business.DependecyResolvers;
using Authentication.Business;
using Authentication.DataAccess;
using Authentication.WebApi;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Infrastructure.Utilities.IoC.Implementations;
using Infrastructure.Utilities.IoC.Interfaces;
using Infrastructure;
using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });
builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCustomException();

app.Run();

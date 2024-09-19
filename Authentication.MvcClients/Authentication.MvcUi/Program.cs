using Authentication.MvcUi.ApiServices.Implmentations;
using Authentication.MvcUi.ApiServices.Interfaces;
using Authentication.MvcUi.Middlewares;

namespace Authentication.MvcUi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IHttpApiService, HttpApiService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseMiddleware<CacheControlHandler>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseMiddleware<StatusCodeHandler>();

            app.MapControllerRoute(name: "default", pattern: "{controller=Authentication}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

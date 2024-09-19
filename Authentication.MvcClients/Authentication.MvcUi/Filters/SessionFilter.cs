using Authentication.MvcUi.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Authentication.MvcUi.Extensions;
using Authentication.MvcUi.Models.Responses;
using Authentication.MvcUi.Models;

namespace Authentication.MvcUi.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpApiService _httpApiService;
        public SessionFilter(IConfiguration configuration, IHttpApiService httpApiService)
        {
            _configuration = configuration;
            _httpApiService = httpApiService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var activeToken = context.HttpContext.Session.GetObject<TokenResponse>("ActiveToken");
            if (activeToken == null || DateTime.Now >= activeToken.RefreshTokenExpiration)
            {
                ClearSessionAndRedirect(context);
                return;
            }
            if (DateTime.Now >= activeToken.AccessTokenExpiration)
            {
                var refreshedToken = await RefreshToken(activeToken.RefreshToken);
                if (refreshedToken == null)
                {
                    ClearSessionAndRedirect(context);
                    return;
                }
                activeToken = refreshedToken;
            }
            context.HttpContext.Session.SetObject("ActiveToken", activeToken);
            await next();
        }

        private async Task<TokenResponse?> RefreshToken(string refreshToken)
        {
            var postDto = new { token = refreshToken };
            var response = await _httpApiService.PostDataAsync<ResponseBody<TokenResponse>>("BaseAddress", "/Authentication/CreateTokenByRefreshToken", JsonSerializer.Serialize(postDto));
            return response.StatusCode == 201 ? response.Data : null;
        }

        private void ClearSessionAndRedirect(ActionExecutingContext context)
        {
            context.HttpContext.Session.Clear();
            context.Result = new RedirectResult("~/Authentication/Login");
        }
    }
}

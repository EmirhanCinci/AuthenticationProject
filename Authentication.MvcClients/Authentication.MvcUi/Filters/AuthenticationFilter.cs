using Authentication.MvcUi.Extensions;
using Authentication.MvcUi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Authentication.MvcUi.Filters
{
	public class AuthenticationFilter : ActionFilterAttribute
	{
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
			var activeToken = context.HttpContext.Session.GetObject<TokenResponse>("ActiveToken");
			if (activeToken != null)
			{
				if (DateTime.Now <= activeToken?.RefreshTokenExpiration)
				{
					context.Result = new RedirectResult("~/Home/Index");
				}
				else
				{
					await next();
				}
			}
			else
			{
				await next();
			}
		}
	}
}

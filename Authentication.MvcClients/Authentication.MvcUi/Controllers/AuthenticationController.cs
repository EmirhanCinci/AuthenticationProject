using Authentication.MvcUi.ApiServices.Interfaces;
using Authentication.MvcUi.Extensions;
using Authentication.MvcUi.Filters;
using Authentication.MvcUi.Models.Responses;
using Authentication.MvcUi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Authentication.MvcUi.Models.Requests;

namespace Authentication.MvcUi.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpApiService _httpApiService;
        public AuthenticationController(IConfiguration configuration, IHttpApiService httpApiService)
        {
            _configuration = configuration;
            _httpApiService = httpApiService;
        }

        [AuthenticationFilter]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

		[AuthenticationFilter]
		[HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

		[AuthenticationFilter]
		[HttpGet]
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new RedirectResult("~/Utilities/NotFoundPage");
            }
            var dto = new { Code = id };
            var response = await _httpApiService.PostDataAsync<ResponseBody<NoData>>("BaseAddress", "/Authentication/ResetPasswordControl", JsonSerializer.Serialize(dto));
            if (!response.IsSuccessful)
            {
                return new RedirectResult("~/Utilities/NotFoundPage");
            }
            ViewBag.Code = id;
            return View();
        }

		[AuthenticationFilter]
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

		[AuthenticationFilter]
		[HttpPost]
        public async Task<IActionResult> Login(UserRequest.LoginRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<TokenResponse>>("BaseAddress", "/Authentication/Login", JsonSerializer.Serialize(dto));
            if (response.StatusCode == 200 && response.Data != null)
            {
                HttpContext.Session.SetObject("ActiveToken", response.Data);
                return Json(new { IsSuccessful = true, RedirectUrl = "/Home/Index" });
            }
            else
            {
                return Json(response);
            }
        }

		[AuthenticationFilter]
		[HttpPost]
        public async Task<IActionResult> ForgotPassword(UserRequest.ForgotPasswordRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<NoData>>("BaseAddress", "/Authentication/ForgotPassword", JsonSerializer.Serialize(dto));
            return Json(response);
        }

		[AuthenticationFilter]
		[HttpPost]
        public async Task<IActionResult> ResetPassword(UserRequest.ResetPasswordRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<NoData>>("BaseAddress", "/Authentication/ResetPassword", JsonSerializer.Serialize(dto));
            return Json(response);
        }

		[AuthenticationFilter]
		[HttpPost]
        public async Task<IActionResult> Register(UserRequest.UserRegisterRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<UserResponse>>("BaseAddress", "/Authentication/Register", JsonSerializer.Serialize(dto));
            return Json(response);
        }

        [ServiceFilter(typeof(SessionFilter))]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var activeToken = HttpContext.Session.GetObject<TokenResponse>("ActiveToken");
            var dto = new { token = activeToken.RefreshToken };
            var response = await _httpApiService.PostDataAsync<ResponseBody<NoData>>("BaseAddress", "/Authentication/RevokeRefreshToken", JsonSerializer.Serialize(dto));
            HttpContext.Session.Clear();
            return new RedirectResult("~/Authentication/Login");
        }
    }
}

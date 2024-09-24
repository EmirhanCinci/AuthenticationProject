using Authentication.MvcUi.ApiServices.Interfaces;
using Authentication.MvcUi.Filters;
using Authentication.MvcUi.Models;
using Authentication.MvcUi.Models.Requests;
using Authentication.MvcUi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Authentication.MvcUi.Controllers
{
    [ServiceFilter(typeof(SessionFilter))]
    public class UsersController : BaseController
    {
        public UsersController(IConfiguration configuration, IHttpApiService httpApiService) : base(configuration, httpApiService)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(long id)
        {
            var response = await _httpApiService.GetDataAsync<ResponseBody<UserResponse>>("BaseAddress", $"/Users/{id}", _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers(UserRequest.UserFilterRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<Paginate<UserResponse>>>("BaseAddress", "/Users/GetList", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest.UserPostRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<UserResponse>>("BaseAddress", "/Users", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserRequest.UserPutRequest dto)
        {
            var response = await _httpApiService.PutDataAsync<ResponseBody<NoData>>("BaseAddress", "/Users", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoData>>("BaseAddress", $"/Users/{id}", _tokenResponse.AccessToken);
            return Json(response);
        }
    }
}

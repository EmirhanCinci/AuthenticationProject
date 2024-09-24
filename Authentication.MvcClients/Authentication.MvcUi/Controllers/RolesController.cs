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
    public class RolesController : BaseController
    {
        public RolesController(IConfiguration configuration, IHttpApiService httpApiService) : base(configuration, httpApiService)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _httpApiService.GetDataAsync<ResponseBody<RoleResponse>>("BaseAddress", "/Roles", _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetRoles(RoleRequest.RoleFilterRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<List<RoleResponse>>>("BaseAddress", "/Roles/GetList", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleRequest.RolePostRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<RoleResponse>>("BaseAddress", "/Roles", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleRequest.RolePutRequest dto)
        {
            var response = await _httpApiService.PutDataAsync<ResponseBody<NoData>>("BaseAddress", "/Roles", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _httpApiService.DeleteDataAsync<ResponseBody<NoData>>("BaseAddress", "/Roles", _tokenResponse.AccessToken);
            return Json(response);
        }
    }
}

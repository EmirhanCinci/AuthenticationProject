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
    public class UserRolesController : BaseController
    {
        public UserRolesController(IConfiguration configuration, IHttpApiService httpApiService) : base(configuration, httpApiService)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetUserRoles(UserRoleRequest.UserRoleFilterRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<Paginate<UserRoleResponse>>>("BaseAddress", "/UserRoles/GetList", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateUserRoles(UserRoleRequest.UserRolePostAndPutRequest dto)
        {
            var response = await _httpApiService.PostDataAsync<ResponseBody<NoData>>("BaseAddress", "/UserRoles/AddOrUpdate", JsonSerializer.Serialize(dto), _tokenResponse.AccessToken);
            return Json(response);
        }
    }
}

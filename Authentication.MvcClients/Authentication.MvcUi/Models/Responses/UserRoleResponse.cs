using Authentication.MvcUi.Models.Items;

namespace Authentication.MvcUi.Models.Responses
{
    public class UserRoleResponse : BaseResponse<long>
    {
        public long UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}

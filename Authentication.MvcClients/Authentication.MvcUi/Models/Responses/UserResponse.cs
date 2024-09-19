using Authentication.MvcUi.Models.Items;

namespace Authentication.MvcUi.Models.Responses
{
    public class UserResponse : BaseResponse<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsTwoFactorEnabled { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
    }
}

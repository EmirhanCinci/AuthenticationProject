using Authentication.MvcUi.Models.Items;

namespace Authentication.MvcUi.Models.Requests
{
    public class UserRequest
    {
        public class UserPostRequest
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        public class UserRegisterRequest
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class UserPutRequest
        {
            public long Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        public class UserFilterRequest : BasePaginateFilterRequest
        {
            public bool? IsTwoFactorEnabled { get; set; }
            public bool? IsBlocked { get; set; }
        }

        public class LoginRequest
        {
            public string UserNameOrEmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class ChangePasswordRequest
        {
            public long UserId { get; set; }
            public string OldPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ControlNewPassword { get; set; } = string.Empty;
        }

        public class ForgotPasswordRequest
        {
            public string UserName { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class ResetPasswordRequest
        {
            public string Code { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ControlNewPassword { get; set; } = string.Empty;
        }

        public class ResetPasswordControlRequest
        {
            public string Code { get; set; } = string.Empty;
        }
    }
}

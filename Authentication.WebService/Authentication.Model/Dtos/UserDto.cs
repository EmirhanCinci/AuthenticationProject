using Infrastructure.Model.Dtos.Implementations;
using Infrastructure.Model.Dtos.Interfaces;

namespace Authentication.Model.Dtos
{
    public class UserDto
    {
        public class UserGetDto : BaseDto<long>
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

        public class UserPostDto : IDto
        {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class UserPutDto : IDto
        {
            public long Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
        }

        public class UserFilterDto : BasePaginateFilterDto
        {
            public bool? IsTwoFactorEnabled { get; set; }
            public bool? IsBlocked { get; set; }
        }

        public class LoginDto : IDto
        {
            public string UserNameOrEmail { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class ChangePasswordDto : IDto
        {
            public long UserId { get; set; }
            public string OldPassword { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ControlNewPassword { get; set; } = string.Empty;
        }

        public class ForgotPasswordDto : IDto
        {
            public string UserName { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class ResetPasswordDto : IDto
        {
            public string Code { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ControlNewPassword { get; set; } = string.Empty;
        }

        public class ResetPasswordControlDto : IDto
        {
            public string Code { get; set; } = string.Empty;
        }
    }
}

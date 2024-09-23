using Authentication.Business.Constants;
using Authentication.Model.Dtos;
using FluentValidation;

namespace Authentication.Business.Validations
{
    public class UserRoleDtoValidator
    {
        public class UserRolePostAndPutDtoValidator : AbstractValidator<UserRoleDto.UserRolePostAndPutDto> 
        {
            public UserRolePostAndPutDtoValidator()
            {
                RuleFor(x => x.UserId)
                    .NotNull().NotEmpty().WithMessage(UserRoleMessages.NotEmptyUserId);

                RuleFor(x => x.RoleIdList)
                    .NotNull().WithMessage(UserRoleMessages.NotEmptyRoleId);
            }
        }
    }
}

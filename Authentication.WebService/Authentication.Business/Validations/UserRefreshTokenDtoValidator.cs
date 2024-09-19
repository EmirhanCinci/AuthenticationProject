using Authentication.Business.Constants;
using Authentication.Model.Dtos;
using FluentValidation;

namespace Authentication.Business.Validations
{
    public class UserRefreshTokenDtoValidator : AbstractValidator<UserRefreshTokenDto>
    {
        public UserRefreshTokenDtoValidator()
        {
            RuleFor(x => x.Token)
                .NotNull().NotEmpty().WithMessage(AuthenticationMessages.NotEmptyRefreshToken);
        }
    }
}

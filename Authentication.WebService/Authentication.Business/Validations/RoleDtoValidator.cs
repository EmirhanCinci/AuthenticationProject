using Authentication.Business.Constants;
using Authentication.Model.Dtos;
using FluentValidation;
using Infrastructure.Constants;

namespace Authentication.Business.Validations
{
    public class RoleDtoValidator
    {
        public class RolePostDtoValidator : AbstractValidator<RoleDto.RolePostDto>
        {
            public RolePostDtoValidator()
            {
                RuleFor(x => x.Name)
                   .NotNull().NotEmpty().WithMessage(RoleMessages.NotEmptyName)
                   .MinimumLength(2).WithMessage(RoleMessages.InvalidNameMinLength)
                   .MaximumLength(100).WithMessage(RoleMessages.InvalidNameMaxLength);
            }
        }

        public class RolePutDtoValidator : AbstractValidator<RoleDto.RolePutDto>
        {
            public RolePutDtoValidator()
            {
                RuleFor(x => x.Id)
                  .NotNull().NotEmpty().WithMessage(SystemMessages.NotEmptyId)
                  .GreaterThan(0).WithMessage(SystemMessages.IdGreaterThanZero);

                RuleFor(x => x.Name)
                   .NotNull().NotEmpty().WithMessage(RoleMessages.NotEmptyName)
                   .MinimumLength(2).WithMessage(RoleMessages.InvalidNameMinLength)
                   .MaximumLength(100).WithMessage(RoleMessages.InvalidNameMaxLength);
            }
        }
    }
}

using Authentication.Business.Constants;
using Authentication.Model.Dtos;
using FluentValidation;
using Infrastructure.Constants;
using Infrastructure.Extensions;

namespace Authentication.Business.Validations
{
    public class UserDtoValidator
    {
        public class UserPostDtoValidator : AbstractValidator<UserDto.UserPostDto>
        {
            public UserPostDtoValidator()
            {
                RuleFor(x => x.FirstName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyFirstName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidFirstNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidFirstNameMaxLength);

                RuleFor(x => x.LastName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyLastName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidLastNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidLastNameMaxLength);

                RuleFor(x => x.UserName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyUserName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidUserNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidUserNameMaxLength);

                RuleFor(x => x.Email)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyEmail)
                    .EmailAddress().WithMessage(UserMessages.InvalidEmailFormat)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidEmailMaxLength);

                RuleFor(x => x.Phone)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyPhoneNumber)
                    .Length(15).WithMessage(UserMessages.InvalidPhoneNumberLength);
            }
        }

        public class UserPutDtoValidator : AbstractValidator<UserDto.UserPutDto>
        {
            public UserPutDtoValidator()
            {
                RuleFor(x => x.Id)
                   .NotNull().NotEmpty().WithMessage(SystemMessages.NotEmptyId)
                   .GreaterThan(0).WithMessage(SystemMessages.IdGreaterThanZero);

                RuleFor(x => x.FirstName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyFirstName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidFirstNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidFirstNameMaxLength);

                RuleFor(x => x.LastName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyLastName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidLastNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidLastNameMaxLength);

                RuleFor(x => x.UserName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyUserName)
                    .MinimumLength(2).WithMessage(UserMessages.InvalidUserNameMinLength)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidUserNameMaxLength);

                RuleFor(x => x.Email)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyEmail)
                    .EmailAddress().WithMessage(UserMessages.InvalidEmailFormat)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidEmailMaxLength);

                RuleFor(x => x.Phone)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyPhoneNumber)
                    .Length(15).WithMessage(UserMessages.InvalidPhoneNumberLength);
            }
        }

        public class ChangePasswordDtoValidator : AbstractValidator<UserDto.ChangePasswordDto>
        {
            public ChangePasswordDtoValidator()
            {
                RuleFor(x => x.UserId)
                    .NotNull().NotEmpty().WithMessage(SystemMessages.NotEmptyId)
                    .GreaterThan(0).WithMessage(SystemMessages.IdGreaterThanZero);

                RuleFor(x => x.OldPassword)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyOldPassword);

                RuleFor(x => x.NewPassword)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyNewPassword)
                    .Must(x => x.ContainDigit()).WithMessage(UserMessages.PasswordContainDigit)
                    .Must(x => x.ContainLowerCase()).WithMessage(UserMessages.PasswordContainLowerCase)
                    .Must(x => x.ContainUpperCase()).WithMessage(UserMessages.PasswordContainUpperCase)
                    .Must(x => x.ContainSpecialCharacter()).WithMessage(UserMessages.PasswordContainSpecialCharacter)
                    .MinimumLength(8).WithMessage(UserMessages.InvalidPasswordMinLength)
                    .MaximumLength(25).WithMessage(UserMessages.InvalidPasswordMaxLength);

                RuleFor(x => x.ControlNewPassword)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyControlNewPassword)
                    .Equal(x => x.NewPassword).WithMessage(AuthenticationMessages.NotEqualsNewPasswords);
            }
        }

        public class ForgotPasswordDtoDtoValidator : AbstractValidator<UserDto.ForgotPasswordDto>
        {
            public ForgotPasswordDtoDtoValidator()
            {
                RuleFor(x => x.UserName)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyUserName)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidUserNameMaxLength);

                RuleFor(x => x.Email)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyEmail)
                    .EmailAddress().WithMessage(UserMessages.InvalidEmailFormat)
                    .MaximumLength(100).WithMessage(UserMessages.InvalidEmailMaxLength);
            }
        }

        public class ResetPasswordDtoValidator : AbstractValidator<UserDto.ResetPasswordDto>
        {
            public ResetPasswordDtoValidator()
            {
                RuleFor(x => x.Code)
                    .NotNull().NotEmpty().WithMessage(AuthenticationMessages.NotEmptyResetPasswordCode);

                RuleFor(x => x.NewPassword)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyNewPassword)
                    .Must(x => x.ContainDigit()).WithMessage(UserMessages.PasswordContainDigit)
                    .Must(x => x.ContainLowerCase()).WithMessage(UserMessages.PasswordContainLowerCase)
                    .Must(x => x.ContainUpperCase()).WithMessage(UserMessages.PasswordContainUpperCase)
                    .Must(x => x.ContainSpecialCharacter()).WithMessage(UserMessages.PasswordContainSpecialCharacter)
                    .MinimumLength(8).WithMessage(UserMessages.InvalidPasswordMinLength)
                    .MaximumLength(25).WithMessage(UserMessages.InvalidPasswordMaxLength);

                RuleFor(x => x.ControlNewPassword)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyControlNewPassword)
                    .Equal(x => x.NewPassword).WithMessage(AuthenticationMessages.NotEqualsNewPasswords);
            }
        }

        public class ResetPasswordControlDtoValidator : AbstractValidator<UserDto.ResetPasswordControlDto>
        {
            public ResetPasswordControlDtoValidator()
            {
                RuleFor(x => x.Code)
                    .NotNull().NotEmpty().WithMessage(AuthenticationMessages.InvalidForgotPasswordRequest);
            }
        }

        public class LoginDtoValidator : AbstractValidator<UserDto.LoginDto>
        {
            public LoginDtoValidator()
            {
                RuleFor(x => x.UserNameOrEmail)
                    .NotNull().NotEmpty().WithMessage(AuthenticationMessages.NotEmptyLogin);

                RuleFor(x => x.Password)
                    .NotNull().NotEmpty().WithMessage(UserMessages.NotEmptyPassword);
            }
        }
    }
}

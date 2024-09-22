using Authentication.Business.BusinessRules;
using Authentication.Business.Constants;
using Authentication.Business.Interfaces;
using Authentication.Business.Profiles;
using Authentication.Business.Utilities.Email;
using Authentication.Business.Utilities.Security.Jwt.Dtos;
using Authentication.Business.Utilities.Security.Jwt.Interfaces;
using Authentication.Business.Validations;
using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Dtos;
using Authentication.Model.Entities;
using Infrastructure.Aspects;
using Infrastructure.Constants;
using Infrastructure.CrossCuttingConcerns.Exceptions;
using Infrastructure.DataAccess.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Utilities.Email.Dtos;
using Infrastructure.Utilities.Email.Interfaces;
using Infrastructure.Utilities.IoC;
using Infrastructure.Utilities.Responses;
using Infrastructure.Utilities.Security.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Business.Implementations
{
	[DtoNullCheckAspect]
    [PerformanceAspect(5)]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IResetPasswordRequestRepository _resetPasswordRequestRepository;
        private readonly IPasswordHistoryRepository _passwordHistoryRepository;
        private readonly IUnitOfWork<AuthenticationDbContext> _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserBusinessRules _userBusinessRules;
        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository, IUserRefreshTokenRepository userRefreshTokenRepository, IResetPasswordRequestRepository resetPasswordRequestRepository, IPasswordHistoryRepository passwordHistoryRepository, IUnitOfWork<AuthenticationDbContext> unitOfWork, ITokenService tokenService, IEmailService emailService, UserBusinessRules userBusinessRules)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _resetPasswordRequestRepository = resetPasswordRequestRepository;
            _passwordHistoryRepository = passwordHistoryRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _emailService = emailService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userBusinessRules = userBusinessRules;
        }

        [ValidationAspect(typeof(UserDtoValidator.ChangePasswordDtoValidator))]
        public async Task<CustomApiResponse<NoData>> ChangePasswordAsync(UserDto.ChangePasswordDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId, false, false, default) ?? throw new BadRequestException(UserMessages.NotFoundById);
            var isValidPassword = PasswordHelper.VerifyPasswordHashByHmacSha512(dto.OldPassword, user.PasswordHash, user.PasswordSalt) ? true : throw new BadRequestException(AuthenticationMessages.TryControlNewPassword);
            var passwordHistories = await _passwordHistoryRepository.Queryable(prd => prd.UserId == dto.UserId && prd.IsDeleted == false).OrderByDescending(x => x.CreatedDate).Take(3).ToListAsync();
            var passwordControl = passwordHistories.Any(item => PasswordHelper.VerifyPasswordHashByHmacSha512(dto.NewPassword, item.PasswordHash, item.PasswordSalt)) ? throw new BadRequestException(AuthenticationMessages.LastThreePasswordException) : true;
            if (passwordHistories.Count == 3)
            {
                var lastPassword = passwordHistories.Last();
                await _passwordHistoryRepository.DeleteAsync(lastPassword);
                await _unitOfWork.CommitAsync();
            }
            (user.PasswordHash, user.PasswordSalt) = PasswordHelper.CreatePasswordByHmacSha512(dto.NewPassword);
            await _passwordHistoryRepository.AddAsync(new PasswordHistory { UserId = user.Id, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt });
            await _unitOfWork.CommitAsync();
            await _userRepository.UpdateAsync(user);
            var mailResponse = await _emailService.SendEmailAsync(new EmailDto.EmailPostDto { ReceiverEmail = user.Email, Subject = EmailTemplate.ChangePasswordTitle, Body = EmailTemplate.ChangePasswordEmailTemplate(user.FirstName, user.LastName) });
            var result = mailResponse.IsSuccess ? true : throw new Exception(SystemMessages.InternalServerError);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, AuthenticationMessages.SuccessChangePassword);
        }

        [ValidationAspect(typeof(UserRefreshTokenDtoValidator))]
        public async Task<CustomApiResponse<TokenDto>> CreateTokenByRefreshTokenAsync(UserRefreshTokenDto dto)
        {
            var existRefreshToken = await _userRefreshTokenRepository.GetAsync(prd => prd.Code == dto.Token && prd.IsDeleted == false) ?? throw new NotFoundException(AuthenticationMessages.RefreshTokenNotFound);
            var user = await _userRepository.GetByIdAsync(existRefreshToken.UserId, false, false, default) ?? throw new BadRequestException(UserMessages.NotFoundById);
            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpiration;
            existRefreshToken.UpdatedDate = DateTime.Now;
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<TokenDto>.Success(StatusCodes.Status201Created, tokenDto, AuthenticationMessages.SuccessCreateToken);
        }

        [ValidationAspect(typeof(UserDtoValidator.ForgotPasswordDtoDtoValidator))]
        public async Task<CustomApiResponse<NoData>> ForgotPasswordAsync(UserDto.ForgotPasswordDto dto)
        {
            var user = await _userRepository.GetAsync(prd => prd.Email == dto.Email && prd.UserName == dto.UserName) ?? throw new NotFoundException(AuthenticationMessages.InformationNotFound);
            var passwordRequest = await _resetPasswordRequestRepository.AnyAsync(prd => prd.UserId == user.Id && prd.CreatedDate <= DateTime.Now && prd.ExpirationDate >= DateTime.Now && prd.IsDeleted == false) ? throw new BadRequestException(AuthenticationMessages.ForgotPasswordRequestExists) : true;
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var resetCode = _tokenService.CreatePasswordResetToken(user);
            var request = new ResetPasswordRequest { UserId = user.Id, IpAddress = ipAddress, ResetCode = resetCode };
            var inserted = await _resetPasswordRequestRepository.AddAsync(request);
            var mailResponse = await _emailService.SendEmailAsync(new EmailDto.EmailPostDto { ReceiverEmail = dto.Email, Subject = EmailTemplate.ForgotPasswordTitle, Body = EmailTemplate.ForgotPasswordEmailTemplate(user.FirstName, user.LastName, $"{_configuration["ClientUrls:Mvc"]}/Authentication/ResetPassword/{inserted.ResetCode}") });
            var result = mailResponse.IsSuccess ? true : throw new Exception(SystemMessages.InternalServerError);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, AuthenticationMessages.SuccessForgotPassword);
        }

        [ValidationAspect(typeof(UserDtoValidator.LoginDtoValidator))]
        public async Task<CustomApiResponse<TokenDto>> LoginAsync(UserDto.LoginDto dto)
        {
            var user = await _userRepository.GetAsync(prd => (dto.UserNameOrEmail.IsValidEmail() ? prd.Email == dto.UserNameOrEmail : prd.UserName == dto.UserNameOrEmail) && prd.IsDeleted == false) ?? throw new BadRequestException(AuthenticationMessages.WrongLogin);
            var isValidPassword = PasswordHelper.VerifyPasswordHashByHmacSha512(dto.Password, user.PasswordHash, user.PasswordSalt) ? true : throw new BadRequestException(AuthenticationMessages.WrongLogin);
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenRepository.GetAsync(prd => prd.UserId == user.Id && prd.IsDeleted == false);
            if (userRefreshToken == null)
            {
                await _userRefreshTokenRepository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, ExpirationDate = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.ExpirationDate = token.RefreshTokenExpiration;
                userRefreshToken.UpdatedDate = DateTime.Now;
            }
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<TokenDto>.Success(StatusCodes.Status200OK, token, AuthenticationMessages.SuccessLogin);
        }

        [ValidationAspect(typeof(UserDtoValidator.UserRegisterDtoValidator))]
		[TransactionScopeAspect]
		public async Task<CustomApiResponse<UserDto.UserGetDto>> RegisterAsync(UserDto.UserRegisterDto dto)
        {
            await _userBusinessRules.AddUniqueControlAsync(dto);
            var user = CustomObjectMapper.Mapper.Map<User>(dto);
            user.FirstName = dto.FirstName.ToTitleCase();
            user.LastName = dto.LastName.ToUpper();
            (user.PasswordHash, user.PasswordSalt) = PasswordHelper.CreatePasswordByHmacSha512(dto.Password);
            var inserted = await _userRepository.AddAsync(user);
			await _unitOfWork.CommitAsync();
			await _passwordHistoryRepository.AddAsync(new PasswordHistory { UserId = inserted.Id, PasswordHash = inserted.PasswordHash, PasswordSalt = inserted.PasswordSalt });
			await _unitOfWork.CommitAsync();
            throw new BadRequestException("hata");
            var mailResponse = await _emailService.SendEmailAsync(new EmailDto.EmailPostDto { ReceiverEmail = dto.Email, Subject = EmailTemplate.PasswordTitle, Body = EmailTemplate.RegisterEmailTemplate(user.FirstName, user.LastName) });
            var result = mailResponse.IsSuccess ? true : throw new Exception(SystemMessages.InternalServerError);
            var mappedDto = CustomObjectMapper.Mapper.Map<UserDto.UserGetDto>(user);
            return CustomApiResponse<UserDto.UserGetDto>.Success(StatusCodes.Status201Created, mappedDto, AuthenticationMessages.SuccessRegister);
        }

        [ValidationAspect(typeof(UserDtoValidator.ResetPasswordDtoValidator))]
        public async Task<CustomApiResponse<NoData>> ResetPasswordAsync(UserDto.ResetPasswordDto dto)
        {
            var resetRequest = await _resetPasswordRequestRepository.GetAsync(prd => prd.ResetCode == dto.Code && prd.ExpirationDate >= DateTime.Now && prd.IsDeleted == false) ?? throw new BadRequestException(AuthenticationMessages.InvalidForgotPasswordRequest); ;
            var user = await _userRepository.GetByIdAsync(resetRequest.UserId, false, false, default) ?? throw new BadRequestException(UserMessages.NotFoundById);
            var passwordHistories = await _passwordHistoryRepository.Queryable(prd => prd.UserId == user.Id && prd.IsDeleted == false).OrderByDescending(x => x.CreatedDate).Take(3).ToListAsync();
            var passwordControl = passwordHistories.Any(item => PasswordHelper.VerifyPasswordHashByHmacSha512(dto.NewPassword, item.PasswordHash, item.PasswordSalt)) ? throw new BadRequestException(AuthenticationMessages.LastThreePasswordException) : true;
            if (passwordHistories.Count == 3)
            {
                var lastPassword = passwordHistories.Last();
                await _passwordHistoryRepository.DeleteAsync(lastPassword);
                await _unitOfWork.CommitAsync();
            }
            (user.PasswordHash, user.PasswordSalt) = PasswordHelper.CreatePasswordByHmacSha512(dto.NewPassword);
            await _passwordHistoryRepository.AddAsync(new PasswordHistory { UserId = user.Id, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt });
			await _unitOfWork.CommitAsync();
			(user.PasswordHash, user.PasswordSalt) = PasswordHelper.CreatePasswordByHmacSha512(dto.NewPassword);
            await _resetPasswordRequestRepository.DeleteAsync(resetRequest);
            await _unitOfWork.CommitAsync();
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, AuthenticationMessages.SuccessResetPassword);
        }

        [ValidationAspect(typeof(UserDtoValidator.ResetPasswordControlDtoValidator))]
        public async Task<CustomApiResponse<NoData>> ResetPasswordControlAsync(UserDto.ResetPasswordControlDto dto)
        {
            var resetRequest = await _resetPasswordRequestRepository.GetAsync(prd => prd.ResetCode == dto.Code && prd.ExpirationDate >= DateTime.Now && prd.IsDeleted == false) ?? throw new BadRequestException(AuthenticationMessages.InvalidForgotPasswordRequest);
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        [ValidationAspect(typeof(UserRefreshTokenDtoValidator))]
        public async Task<CustomApiResponse<NoData>> RevokeRefreshTokenAsync(UserRefreshTokenDto dto)
        {
            var existRefreshToken = await _userRefreshTokenRepository.GetAsync(prd => prd.Code == dto.Token && prd.IsDeleted == false) ?? throw new NotFoundException(AuthenticationMessages.RefreshTokenNotFound);
            await _userRefreshTokenRepository.DeleteAsync(existRefreshToken, false);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, AuthenticationMessages.SuccessDeleteToken);
        }
    }
}

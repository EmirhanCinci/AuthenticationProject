using Authentication.Business.BusinessRules;
using Authentication.Business.Constants;
using Authentication.Business.Interfaces;
using Authentication.Business.Profiles;
using Authentication.Business.Utilities.Email;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Business.Implementations
{
    [PerformanceAspect(5)]
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHistoryRepository _passwordHistoryRepository;
        private readonly IUnitOfWork<AuthenticationDbContext> _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserBusinessRules _userBusinessRules;
        public UserService(IConfiguration configuration, IUserRepository userRepository, IPasswordHistoryRepository passwordHistoryRepository, IUnitOfWork<AuthenticationDbContext> unitOfWork, IEmailService emailService, UserBusinessRules userBusinessRules)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _passwordHistoryRepository = passwordHistoryRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userBusinessRules = userBusinessRules;
        }

        [DtoNullCheckAspect]
        [ValidationAspect(typeof(UserDtoValidator.UserPostDtoValidator))]
		[TransactionScopeAspect]
		public async Task<CustomApiResponse<UserDto.UserGetDto>> AddAsync(UserDto.UserPostDto dto)
        {
            await _userBusinessRules.AddUniqueControlAsync(dto);
            var user = CustomObjectMapper.Mapper.Map<User>(dto);
            user.FirstName = dto.FirstName.ToTitleCase();
            user.LastName = dto.LastName.ToUpper();
            var password = PasswordGenerator.GeneratePassword();
            (user.PasswordHash, user.PasswordSalt) = PasswordHelper.CreatePasswordByHmacSha512(password);
            var inserted = await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();
            await _passwordHistoryRepository.AddAsync(new PasswordHistory { UserId = inserted.Id, PasswordHash = user.PasswordHash, PasswordSalt = user.PasswordSalt });
            var mailResponse = await _emailService.SendEmailAsync(new EmailDto.EmailPostDto { ReceiverEmail = dto.Email, Subject = EmailTemplate.PasswordTitle, Body = EmailTemplate.PasswordEmailTemplate(user.FirstName, user.LastName, user.UserName, user.Email, password) });
            var result = mailResponse.IsSuccess ? true : throw new Exception(SystemMessages.InternalServerError);
            await _unitOfWork.CommitAsync();
            var mappedDto = CustomObjectMapper.Mapper.Map<UserDto.UserGetDto>(user);
            return CustomApiResponse<UserDto.UserGetDto>.Success(StatusCodes.Status201Created, mappedDto, UserMessages.AddedUser);
        }

        [IdCheckAspect]
        public async Task<CustomApiResponse<NoData>> DeleteAsync(long id, bool permanent = false)
        {
            var user = await _userRepository.GetByIdAsync(id, false, false, default) ?? throw new BadRequestException(UserMessages.NotFoundById);
            await _userRepository.DeleteAsync(user, permanent);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, UserMessages.DeletedUser);
        }

        [IdCheckAspect] 
        public async Task<CustomApiResponse<UserDto.UserGetDto>> GetByIdAsync(long id, params string[] includeList)
        {
            var user = await _userRepository.GetByIdAsync(id, false, false, default, includeList) ?? throw new BadRequestException(UserMessages.NotFoundById);
            var mappedDto = CustomObjectMapper.Mapper.Map<UserDto.UserGetDto>(user);
            return CustomApiResponse<UserDto.UserGetDto>.Success(StatusCodes.Status200OK, mappedDto, UserMessages.GetUser);
        }

        [DtoNullCheckAspect]
        public async Task<CustomApiResponse<Paginate<UserDto.UserGetDto>>> GetListAsync(UserDto.UserFilterDto dto, params string[] includeList)
        {
            var queryable = _userRepository.Queryable(prd => (!dto.IsBlocked.HasValue || prd.IsBlocked == dto.IsBlocked) && (!dto.IsTwoFactorEnabled.HasValue || prd.IsTwoFactorEnabled == dto.IsTwoFactorEnabled), false, dto.IsDeleted, includeList);
            var userList = await queryable.ToPaginateAsync(dto.Index, dto.Size) ?? throw new NotFoundException(UserMessages.NotFound);
            var mappedDto = CustomObjectMapper.Mapper.Map<Paginate<UserDto.UserGetDto>>(userList);
            return CustomApiResponse<Paginate<UserDto.UserGetDto>>.Success(StatusCodes.Status200OK, mappedDto, UserMessages.GetUsers);
        }

        [DtoNullCheckAspect]
        [ValidationAspect(typeof(UserDtoValidator.UserPutDtoValidator))]
        public async Task<CustomApiResponse<NoData>> UpdateAsync(UserDto.UserPutDto dto)
        {
            await _userBusinessRules.UpdateUniqueControlAsync(dto);
            var user = await _userRepository.GetByIdAsync(dto.Id, false, false, default) ?? throw new BadRequestException(UserMessages.NotFoundById);
            var mappedDto = CustomObjectMapper.Mapper.Map<User>(dto);
            mappedDto.FirstName = dto.FirstName.ToTitleCase();
            mappedDto.LastName = dto.LastName.ToUpper();
            mappedDto.CreatedDate = user.CreatedDate;
            mappedDto.PasswordHash = user.PasswordHash;
            mappedDto.PasswordSalt = user.PasswordSalt;
            await _userRepository.UpdateAsync(mappedDto);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, UserMessages.UpdatedUser);
        }
    }
}

using Authentication.Business.Aspects;
using Authentication.Business.BusinessRules;
using Authentication.Business.Constants;
using Authentication.Business.Interfaces;
using Authentication.Business.Profiles;
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
using Infrastructure.Utilities.IoC;
using Infrastructure.Utilities.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Business.Implementations
{
    [PerformanceAspect(5)]
    public class UserRoleService : IUserRoleService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork<AuthenticationDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRoleBusinessRules _userRoleBusinessRules;
        public UserRoleService(IConfiguration configuration, IUserRoleRepository userRoleRepository, IUnitOfWork<AuthenticationDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor, UserRoleBusinessRules userRoleBusinessRules)
        {
            _configuration = configuration;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _userRoleBusinessRules = userRoleBusinessRules;
        }

        [DtoNullCheckAspect]
        [ValidationAspect(typeof(UserRoleDtoValidator.UserRolePostAndPutDtoValidator))]
        [SecuredOperationAspect("Admin,Admin Create,Admin Delete,Admin Update")]
        [TransactionScopeAspect]
        public async Task<CustomApiResponse<NoData>> AddOrUpdateAsync(UserRoleDto.UserRolePostAndPutDto dto)
        {
            var userRoles = await _userRoleRepository.Queryable(prd => prd.UserId == dto.UserId, false, false).Select(x => x.RoleId).ToListAsync();
            var addedList = new List<UserRole>();
            var rolesToAdd = dto.RoleIdList.Except(userRoles).ToList();
            var removedList = new List<UserRole>(); 
            var rolesToRemove = userRoles.Except(dto.RoleIdList).ToList();
            foreach (var roleId in rolesToAdd)
            {
                var newUserRole = new UserRole { UserId = dto.UserId, RoleId = roleId };
                addedList.Add(newUserRole);
            }
            await _userRoleRepository.AddRangeAsync(addedList);
            await _unitOfWork.CommitAsync();
            foreach (var roleId in rolesToRemove)
            {
                var userRoleToDelete = await _userRoleRepository.Queryable(prd => prd.UserId == dto.UserId && prd.RoleId == roleId, false, false).FirstOrDefaultAsync();
                if (userRoleToDelete != null)
                {
                    removedList.Add(userRoleToDelete);
                }
            }
            await _userRoleRepository.DeleteRangeAsync(removedList);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, SystemMessages.OperationSuccessful);
        }

        [DtoNullCheckAspect]
        [SecuredOperationAspect("Admin,Admin Read")]
        public async Task<CustomApiResponse<Paginate<UserRoleDto.UserRoleGetDto>>> GetListAsync(UserRoleDto.UserRoleFilterDto dto, params string[] includeList)
        {
            var queryable = _userRoleRepository.Queryable(prd => (!dto.UserId.HasValue || prd.UserId == dto.UserId) && (!dto.RoleId.HasValue || prd.RoleId == dto.RoleId), false, dto.IsDeleted, includeList);
            var userRoleList = await queryable.ToPaginateAsync(dto.Index, dto.Size) ?? throw new NotFoundException(UserRoleMessages.NotFound);
            var mappedDto = CustomObjectMapper.Mapper.Map<Paginate<UserRoleDto.UserRoleGetDto>>(userRoleList);
            return CustomApiResponse<Paginate<UserRoleDto.UserRoleGetDto>>.Success(StatusCodes.Status200OK, mappedDto, UserRoleMessages.GetUserRoles);
        }
    }
}

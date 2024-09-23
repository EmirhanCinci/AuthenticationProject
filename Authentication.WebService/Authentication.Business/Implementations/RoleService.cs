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
    public class RoleService : IRoleService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<AuthenticationDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleBusinessRules _roleBusinessRules;
        public RoleService(IConfiguration configuration, IRoleRepository roleRepository, IUnitOfWork<AuthenticationDbContext> unitOfWork, IHttpContextAccessor httpContextAccessor, RoleBusinessRules roleBusinessRules)
        {
            _configuration = configuration;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _roleBusinessRules = roleBusinessRules;
        }

        [DtoNullCheckAspect]
        [ValidationAspect(typeof(RoleDtoValidator.RolePostDtoValidator))]
        [SecuredOperationAspect("Admin,Admin Create")]
        public async Task<CustomApiResponse<RoleDto.RoleGetDto>> AddAsync(RoleDto.RolePostDto dto)
        {
            await _roleBusinessRules.AddUniqueControlAsync(dto);
            var role = CustomObjectMapper.Mapper.Map<Role>(dto);
            role.Name = dto.Name.ToTitleCase();
            var inserted = await _roleRepository.AddAsync(role);
            await _unitOfWork.CommitAsync();
            var mappedDto = CustomObjectMapper.Mapper.Map<RoleDto.RoleGetDto>(role);
            return CustomApiResponse<RoleDto.RoleGetDto>.Success(StatusCodes.Status201Created, mappedDto, RoleMessages.AddedRole);
        }

        [IdCheckAspect]
        [SecuredOperationAspect("Admin,Admin Delete")]
        public async Task<CustomApiResponse<NoData>> DeleteAsync(int id, bool permanent = false)
        {
            var role = await _roleRepository.GetByIdAsync(id, false, false, default) ?? throw new BadRequestException(RoleMessages.NotFoundById);
            await _roleRepository.DeleteAsync(role, permanent);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, RoleMessages.DeletedRole);
        }

        [IdCheckAspect]
        [SecuredOperationAspect("Admin,Admin Read")]
        public async Task<CustomApiResponse<RoleDto.RoleGetDto>> GetByIdAsync(int id, params string[] includeList)
        {
            var role = await _roleRepository.GetByIdAsync(id, false, false, default, includeList) ?? throw new BadRequestException(RoleMessages.NotFoundById);
            var mappedDto = CustomObjectMapper.Mapper.Map<RoleDto.RoleGetDto>(role);
            return CustomApiResponse<RoleDto.RoleGetDto>.Success(StatusCodes.Status200OK, mappedDto, RoleMessages.GetRole);
        }

        [DtoNullCheckAspect]
        [SecuredOperationAspect("Admin,Admin Read")]
        public async Task<CustomApiResponse<List<RoleDto.RoleGetDto>>> GetListAsync(RoleDto.RoleFilterDto dto, params string[] includeList)
        {
            var queryable = _roleRepository.Queryable(null, false, dto.IsDeleted, includeList);
            var roleList = await queryable.ToListAsync() ?? throw new NotFoundException(RoleMessages.NotFound);
            var mappedDto = CustomObjectMapper.Mapper.Map<List<RoleDto.RoleGetDto>>(roleList);
            return CustomApiResponse<List<RoleDto.RoleGetDto>>.Success(StatusCodes.Status200OK, mappedDto, RoleMessages.GetRoles);
        }

        [DtoNullCheckAspect]
        [ValidationAspect(typeof(RoleDtoValidator.RolePutDtoValidator))]
        [SecuredOperationAspect("Admin,Admin Update")]
        public async Task<CustomApiResponse<NoData>> UpdateAsync(RoleDto.RolePutDto dto)
        {
            await _roleBusinessRules.UpdateUniqueControlAsync(dto);
            var role = await _roleRepository.GetByIdAsync(dto.Id, false, false, default) ?? throw new BadRequestException(RoleMessages.NotFoundById);
            var mappedDto = CustomObjectMapper.Mapper.Map<Role>(dto);
            mappedDto.Name = dto.Name.ToTitleCase();
            mappedDto.CreatedDate = role.CreatedDate;
            await _roleRepository.UpdateAsync(mappedDto);
            await _unitOfWork.CommitAsync();
            return CustomApiResponse<NoData>.Success(StatusCodes.Status200OK, RoleMessages.UpdatedRole);
        }
    }
}

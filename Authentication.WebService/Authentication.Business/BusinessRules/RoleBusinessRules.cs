using Authentication.Business.Constants;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Dtos;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Authentication.Business.BusinessRules
{
    public class RoleBusinessRules
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
        public RoleBusinessRules(IConfiguration configuration, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _roleRepository = roleRepository;
        }

        public async Task AddUniqueControlAsync(RoleDto.RolePostDto dto)
        {
            var errorList = new List<string>();
            if (await _roleRepository.AnyAsync(prd => prd.Name.ToLower() == dto.Name.ToLower(), false, false))
            {
                errorList.Add(RoleMessages.ExistsName);
            }
            if (errorList != null && errorList.Count > 0)
            {
                var message = string.Join(" - ", errorList);
                throw new ValidationException(message);
            }
        }

        public async Task UpdateUniqueControlAsync(RoleDto.RolePutDto dto)
        {
            var errorList = new List<string>();
            if (await _roleRepository.AnyAsync(prd => prd.Name.ToLower() == dto.Name.ToLower() && prd.Id != dto.Id, false, false))
            {
                errorList.Add(UserMessages.ExistsUserName);
            }
            if (errorList != null && errorList.Count > 0)
            {
                var message = string.Join(" - ", errorList);
                throw new ValidationException(message);
            }
        }
    }
}

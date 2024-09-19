using Authentication.Business.Constants;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Dtos;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace Authentication.Business.BusinessRules
{
    public class UserBusinessRules
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public UserBusinessRules(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task AddUniqueControlAsync(UserDto.UserPostDto dto)
        {
            var errorList = new List<string>();
            if (await _userRepository.AnyAsync(prd => prd.UserName.ToLower() == dto.UserName.ToLower(), false, false))
            {
                errorList.Add(UserMessages.ExistsUserName);
            }
            if (await _userRepository.AnyAsync(prd => prd.Phone == dto.Phone, false, false))
            {
                errorList.Add(UserMessages.ExistsPhone);
            }
            if (await _userRepository.AnyAsync(prd => prd.Email.ToLower() == dto.Email.ToLower(), false, false))
            {
                errorList.Add(UserMessages.ExistsEmail);
            }
            if (errorList != null && errorList.Count > 0)
            {
                var message = string.Join(" - ", errorList);
                throw new ValidationException(message);
            }
        }

        public async Task AddUniqueControlAsync(UserDto.UserRegisterDto dto)
        {
            var errorList = new List<string>();
            if (await _userRepository.AnyAsync(prd => prd.UserName.ToLower() == dto.UserName.ToLower(), false, false))
            {
                errorList.Add(UserMessages.ExistsUserName);
            }
            if (await _userRepository.AnyAsync(prd => prd.Phone == dto.Phone, false, false))
            {
                errorList.Add(UserMessages.ExistsPhone);
            }
            if (await _userRepository.AnyAsync(prd => prd.Email.ToLower() == dto.Email.ToLower(), false, false))
            {
                errorList.Add(UserMessages.ExistsEmail);
            }
            if (errorList != null && errorList.Count > 0)
            {
                var message = string.Join(" - ", errorList);
                throw new ValidationException(message);
            }
        }

        public async Task UpdateUniqueControlAsync(UserDto.UserPutDto dto)
        {
            var errorList = new List<string>();
            if (await _userRepository.AnyAsync(prd => prd.UserName.ToLower() == dto.UserName.ToLower() && prd.Id != dto.Id, false, false))
            {
                errorList.Add(UserMessages.ExistsUserName);
            }
            if (await _userRepository.AnyAsync(prd => prd.Phone == dto.Phone && prd.Id != dto.Id, false, false))
            {
                errorList.Add(UserMessages.ExistsPhone);
            }
            if (await _userRepository.AnyAsync(prd => prd.Email.ToLower() == dto.Email.ToLower() && prd.Id != dto.Id, false, false))
            {
                errorList.Add(UserMessages.ExistsEmail);
            }
            if (errorList != null && errorList.Count > 0)
            {
                var message = string.Join(" - ", errorList);
                throw new ValidationException(message);
            }
        }
    }
}

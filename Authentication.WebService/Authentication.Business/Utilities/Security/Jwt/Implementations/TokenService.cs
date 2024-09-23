using Authentication.Business.Utilities.Security.Jwt.Dtos;
using Authentication.Business.Utilities.Security.Jwt.Interfaces;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.Extensions;
using Infrastructure.Utilities.Security.Encyption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Authentication.Business.Utilities.Security.Jwt.Implementations
{
	public class TokenService : ITokenService
	{
		private readonly TokenOption _tokenOption;
		private readonly IUserRepository _userRepository;
		private readonly IUserRoleRepository _userRoleRepository;
		public TokenService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IOptions<TokenOption> options)
		{
			_userRepository = userRepository;
			_userRoleRepository = userRoleRepository;
			_tokenOption = options.Value;
		}

		private string CreateRefreshToken()
		{
			var numberByte = new byte[32];
			using (var rnd = RandomNumberGenerator.Create())
			{
				rnd.GetBytes(numberByte);
				return Convert.ToBase64String(numberByte);
			}
		}

		private IEnumerable<Claim> SetClaims(User user, List<string> audiences)
		{
			var userRoles = _userRoleRepository.Queryable(prd => prd.UserId == user.Id, false, false, "Role").ToList();
			var claims = new List<Claim>();
			claims.AddNameIdentifier(user.Id.ToString());
			claims.AddEmail(user.Email);
			claims.AddName(user.UserName);
			claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            claims.AddRoles(userRoles.Select(c => c.Role.Name).ToArray());
            return claims;
		}

		public TokenDto CreateToken(User user)
		{
			var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
			var refreshTokenExpiration = DateTime.Now.AddDays(_tokenOption.RefreshTokenExpiration);
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _tokenOption.Issuer, expires: accessTokenExpiration, notBefore: DateTime.Now, claims: SetClaims(user, _tokenOption.Audience), signingCredentials: signingCredentials);
			var handler = new JwtSecurityTokenHandler();
			var token = handler.WriteToken(jwtSecurityToken);
			var tokenDto = new TokenDto
			{
				AccessToken = token,
				RefreshToken = CreateRefreshToken(),
				AccessTokenExpiration = accessTokenExpiration,
				RefreshTokenExpiration = refreshTokenExpiration
			};
			return tokenDto;
		}

		public string CreatePasswordResetToken(User user)
		{
			var tokenExpiration = DateTime.Now.AddMinutes(60);
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.Sub, user.Email), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
			var jwtToken = new JwtSecurityToken(issuer: _tokenOption.Issuer, expires: tokenExpiration, notBefore: DateTime.Now, claims: claims, signingCredentials: signingCredentials);
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(jwtToken);
		}
	}
}

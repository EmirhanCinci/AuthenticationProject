using Infrastructure.Model.Dtos.Interfaces;

namespace Authentication.Model.Dtos
{
    public class UserRefreshTokenDto : IDto
    {
        public string Token { get; set; } = string.Empty;
    }
}

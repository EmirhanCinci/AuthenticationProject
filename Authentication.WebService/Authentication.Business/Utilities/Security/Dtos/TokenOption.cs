namespace Authentication.Business.Utilities.Security.Dtos
{
    public class TokenOption
    {
        public List<string> Audience { get; set; } = new List<string>();
        public string Issuer { get; set; } = string.Empty;
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; } = string.Empty;
    }
}

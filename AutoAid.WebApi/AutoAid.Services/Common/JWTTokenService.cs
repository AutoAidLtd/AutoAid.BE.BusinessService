using AutoAid.Application.Service.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoAid.Bussiness.Common
{
    public class JWTTokenService : ITokenService
    {
        private readonly SigningCredentials _credentials;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JWTTokenService()
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string Encode(GenerateTokenReq data)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AppConfig.JwtSetting.ValidIssuer,
                Audience = AppConfig.JwtSetting.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = _credentials,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, data.Id),
                    new Claim(ClaimTypes.Email, data.Email ?? string.Empty),
                    new Claim(ClaimTypes.Name, data.FullName ?? string.Empty),
                    new Claim(ClaimTypes.MobilePhone, data.Phone ?? string.Empty),
                    new Claim(ClaimTypes.Uri, data.AvatarUrl ?? string.Empty),
                }),
                IssuedAt = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Claim> Decode(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return jwtToken.Claims;
            }
            catch
            {
                return null;
            }
        }
    }
}

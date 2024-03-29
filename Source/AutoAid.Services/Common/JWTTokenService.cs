﻿using AutoAid.Application.Service.Common;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutoAid.Bussiness.Common
{
    public class JWTTokenService : ITokenService
    {
        private SigningCredentials _credentials;
        private SymmetricSecurityKey _securityKey;
        private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JWTTokenService()
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string Encode(GenerateTokenReq data)
        {
            var claimIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, data.Id),
                new Claim(ClaimTypes.Email, data.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, data.FullName ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, data.Phone ?? string.Empty),
                new Claim(ClaimTypes.Uri, data.AvatarUrl ?? string.Empty),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AppConfig.JwtSetting.ValidIssuer,
                Audience = AppConfig.JwtSetting.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = _credentials,
                Subject = claimIdentity,
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _securityKey = null;
                _credentials = null;
                _jwtSecurityTokenHandler = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~JWTTokenService()
        {
            Dispose(false);
        }
    }
}

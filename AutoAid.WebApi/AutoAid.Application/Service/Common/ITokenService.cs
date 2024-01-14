using AutoAid.Domain.Common;
using System.Security.Claims;

namespace AutoAid.Application.Service.Common
{
    public interface ITokenService
    {
        string Encode(GenerateTokenReq data);
        IEnumerable<Claim> Decode(string token);
    }
}

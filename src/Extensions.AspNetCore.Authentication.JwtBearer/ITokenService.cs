using System.Security.Claims;

namespace Extensions.AspNetCore.Authentication.JwtBearer
{
    public interface ITokenService
    {
        string CreateAuthenticationToken(params Claim[] claims);

        string CreateUniqueToken(int length);

        string GenerateSalt();

        string Hash(string value, string salt);
    }
}
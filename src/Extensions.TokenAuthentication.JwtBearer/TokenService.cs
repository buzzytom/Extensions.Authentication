using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Extensions.AspNetCore.Authentication.JwtBearer
{
    public class TokenService : ITokenService
    {
        private static readonly char[] characters = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private readonly ISymmetricKeyProvider provider;
        private readonly JwtConfiguration configuration;

        public TokenService(ISymmetricKeyProvider provider, JwtConfiguration configuration)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public string Hash(string value, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        public string CreateUniqueToken(int length)
        {
            StringBuilder result = new StringBuilder();
            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[length];
                random.GetBytes(data);
                foreach (byte current in data)
                    result.Append(characters[current % characters.Length]);
            }
            return result.ToString();
        }

        public string CreateAuthenticationToken(params Claim[] claims)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(configuration.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(provider.Key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
                Audience = configuration.Audience,
                Issuer = configuration.Issuer
            }));
        }
    }
}

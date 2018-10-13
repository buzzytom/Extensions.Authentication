using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Extensions.Authentication.JwtBearer
{
    /// <summary>
    /// Defines a service that provides operations for handling creating and handling authentication tokens.
    /// </summary>
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

        /// <summary>
        /// Creates a salt to hash with a users password.
        /// </summary>
        /// <returns>A generated salt.</returns>
        public string GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
                random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        /// <summary>
        /// Hashes the given value.
        /// </summary>
        /// <param name="value">The value to hash</param>
        /// <param name="salt">The salt to hash with.</param>
        /// <returns>The hashed value and salt.</returns>
        public string Hash(string value, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        /// <summary>
        /// Creates a unique non-predictable token of a given length.
        /// </summary>
        /// <param name="length">The length of the token to create.</param>
        /// <returns>The created token.</returns>
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

        /// <summary>
        /// Creates a an authentication token for the specified claims.
        /// </summary>
        /// <param name="claims">The claims to include in the authentication token.</param>
        /// <returns>The authentication token.</returns>
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

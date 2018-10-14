using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Extensions.Authentication.JwtBearer
{
    /// <summary>
    /// Defines a service for issuing authentication tokens.
    /// </summary>
    public class JwtBearerTokenService : IBearerTokenService
    {
        private readonly ISymmetricKeyProvider provider;
        private readonly JwtConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtBearerTokenService"/> class, with the specified key provider and JWT configuration.
        /// </summary>
        /// <param name="provider">The provider for the symmetric key used when issuing a JWT.</param>
        /// <param name="configuration">The configuration used to create the issued tokens.</param>
        public JwtBearerTokenService(ISymmetricKeyProvider provider, JwtConfiguration configuration)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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

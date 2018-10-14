using System.Security.Claims;

namespace Extensions.Authentication
{
    /// <summary>
    /// Defines a service for issuing authentication tokens.
    /// </summary>
    public interface IBearerTokenService
    {
        /// <summary>
        /// Creates a an authentication token for the specified claims.
        /// </summary>
        /// <param name="claims">The claims to include in the authentication token.</param>
        /// <returns>The authentication token.</returns>
        string CreateAuthenticationToken(params Claim[] claims);
    }
}

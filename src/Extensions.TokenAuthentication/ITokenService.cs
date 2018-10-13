using System.Security.Claims;

namespace Extensions.AspNetCore.Authentication
{
    /// <summary>
    /// Defines a service that provides operations for handling creating and handling authentication based tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a an authentication token for the specified claims.
        /// </summary>
        /// <param name="claims">The claims to include in the authentication token.</param>
        /// <returns>The authentication token.</returns>
        string CreateAuthenticationToken(params Claim[] claims);

        /// <summary>
        /// Creates a unique non-predictable token of a given length.
        /// </summary>
        /// <param name="length">The length of the token to create.</param>
        /// <returns>The created token.</returns>
        string CreateUniqueToken(int length);

        /// <summary>
        /// Creates a salt to hash with a users password.
        /// </summary>
        /// <returns>A generated salt.</returns>
        string GenerateSalt();

        /// <summary>
        /// Hashes the given value.
        /// </summary>
        /// <param name="value">The value to hash</param>
        /// <param name="salt">The salt to hash with.</param>
        /// <returns>The hashed value and salt.</returns>
        string Hash(string value, string salt);
    }
}
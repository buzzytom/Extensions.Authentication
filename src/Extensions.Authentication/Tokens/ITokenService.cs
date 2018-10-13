namespace Extensions.Authentication
{
    /// <summary>
    /// Defines a service that provides operations for tokens and hashing.
    /// </summary>
    public interface ITokenService
    {
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
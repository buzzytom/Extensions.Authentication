using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Extensions.Authentication
{
    /// <summary>
    /// Defines a service that provides operations for tokens and hashing.
    /// </summary>
    public class TokenService : ITokenService
    {
        private static readonly char[] characters = "abcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

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
            // Microsoft.AspNetCore.Cryptography.KeyDerivation
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
    }
}

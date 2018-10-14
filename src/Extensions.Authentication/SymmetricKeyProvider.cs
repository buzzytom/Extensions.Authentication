using System.Security.Cryptography;

namespace Extensions.Authentication
{
    /// <summary>
    /// A symmetric encryption key provider for the HMAC SHA 256 algorithm.
    /// </summary>
    public class SymmetricKeyProvider : ISymmetricKeyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymmetricKeyProvider"/> class, generating a new key.
        /// </summary>
        public SymmetricKeyProvider()
        {
            Key = new HMACSHA256().Key;
        }

        // ----- Properties ----- //

        /// <summary>Gets the value of symmetric encryption key as a sequence of bytes.</summary>
        public byte[] Key { get; }
    }
}

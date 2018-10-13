using System.Security.Cryptography;

namespace Extensions.AspNetCore.Authentication.JwtBearer
{
    public class SymmetricKeyProvider : ISymmetricKeyProvider
    {
        public SymmetricKeyProvider()
        {
            Key = new HMACSHA256().Key;
        }

        // ----- Properties ----- //

        public byte[] Key { get; }
    }
}

using System;

namespace Extensions.AspNetCore.Authentication.JwtBearer
{
    public interface ISymmetricKeyProvider
    {
        byte[] Key { get; }
    }
}

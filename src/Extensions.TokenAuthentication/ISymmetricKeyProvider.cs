namespace Extensions.AspNetCore.Authentication
{
    /// <summary>
    /// Defines a provider that defines a symmetric encryption key.
    /// </summary>
    public interface ISymmetricKeyProvider
    {
        /// <summary>Gets the value of symmetric encryption key as a sequence of bytes.</summary>
        byte[] Key { get; }
    }
}

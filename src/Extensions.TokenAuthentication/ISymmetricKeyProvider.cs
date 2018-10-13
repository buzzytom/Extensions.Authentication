namespace Extensions.AspNetCore.Authentication
{
    public interface ISymmetricKeyProvider
    {
        byte[] Key { get; }
    }
}

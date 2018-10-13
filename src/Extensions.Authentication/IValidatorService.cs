using System.Collections.Generic;

namespace Extensions.Authentication
{
    public interface IValidatorService
    {
        IEnumerable<string> GetEmailErrors(string email);
        IEnumerable<string> GetPasswordErrors(string password);
    }
}
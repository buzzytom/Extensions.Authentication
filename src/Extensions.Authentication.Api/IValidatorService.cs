using System.Collections.Generic;

namespace Extensions.Authentication
{
    /// <summary>
    /// Defines methods for validating emails and passwords.
    /// </summary>
    public interface IValidatorService
    {
        /// <summary>
        /// Gets a collection of the errors in the specified email address.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>A collection of the errors or an empty collection if the value is valid.</returns>
        IEnumerable<string> GetEmailErrors(string email);

        /// <summary>
        /// Gets a collection of the errors in the specified password.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>A collection of the errors or an empty collection if the value is valid.</returns>
        IEnumerable<string> GetPasswordErrors(string password);
    }
}
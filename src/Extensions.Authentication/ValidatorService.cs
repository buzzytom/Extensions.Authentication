using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Extensions.Authentication
{
    /// <summary>
    /// Defines methods for validating emails and passwords.
    /// </summary>
    public class ValidatorService : IValidatorService
    {
        private readonly Lazy<Regex> emailRegex = new Lazy<Regex>(() => new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.Compiled));
        private readonly PasswordRequirements requirements;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorService"/> class, with the specified password validation rules configuration.
        /// </summary>
        /// <param name="requirements">The validation rules to use when validating a password.</param>
        public ValidatorService(PasswordRequirements requirements)
        {
            this.requirements = requirements ?? throw new ArgumentNullException(nameof(requirements));
        }

        /// <summary>
        /// Gets a collection of the errors in the specified email address.
        /// </summary>
        /// <param name="email">The email address to validate.</param>
        /// <returns>A collection of the errors or an empty collection if the value is valid.</returns>
        public IEnumerable<string> GetEmailErrors(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new string[] { "An email address must be provided." };
            if (!emailRegex.Value.IsMatch(email))
                return new string[] { "Not a valid email address." };

            return new string[0];
        }

        /// <summary>
        /// Gets a collection of the errors in the specified password.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>A collection of the errors or an empty collection if the value is valid.</returns>
        public IEnumerable<string> GetPasswordErrors(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return new string[] { "A password must be specified." };

            List<string> errors = new List<string>();
            if (requirements.MinimumLength.HasValue && password.Length < requirements.MinimumLength)
                errors.Add($"Password must be {requirements.MinimumLength} characters or longer.");
            if (requirements.Lowercase && !Regex.IsMatch(password, "[a-z]"))
                errors.Add("Missing lowercase characters (a-z).");
            if (requirements.Uppercase && !Regex.IsMatch(password, "[A-Z]."))
                errors.Add("Missing uppercase characters (A-Z).");
            if (requirements.Number && !Regex.IsMatch(password, "\\d"))
                errors.Add("Missing numbers (0-9).");
            if (requirements.Special && !Regex.IsMatch(password, "[^A-z\\d]"))
                errors.Add("Missing special characters (e.g. !@#$%^&* ).");

            return errors;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Extensions.Authentication
{
    public class ValidatorService : IValidatorService
    {
        private readonly Lazy<Regex> emailRegex = new Lazy<Regex>(() => new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", RegexOptions.Compiled));
        private readonly PasswordRequirements requirements;

        public ValidatorService(PasswordRequirements requirements)
        {
            this.requirements = requirements ?? throw new ArgumentNullException(nameof(requirements));
        }

        public IEnumerable<string> GetEmailErrors(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return new string[] { "An email address must be provided." };
            if (!emailRegex.Value.IsMatch(email))
                return new string[] { "Not a valid email address." };

            return new string[0];
        }

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

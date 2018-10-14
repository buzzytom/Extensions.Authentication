using System;

namespace Extensions.Authentication
{
    public class PasswordRequirements
    {
        private static PasswordRequirements defaults = null;

        public PasswordRequirements(int maximumLength = 33, int minimumLength = 8, bool lowercase = true, bool uppercase = true, bool number = true, bool special = true)
        {
            if (minimumLength < 0)
                throw new ArgumentException("The miminum length of a password must be greater than or equal to zero.", nameof(minimumLength));
            if (maximumLength <= 0)
                throw new ArgumentException("The maximum length of a password must be greater than zero.", nameof(maximumLength));
            if (maximumLength < minimumLength)
                throw new ArgumentException("The maximum length of a password must be greater than the minimum length.", nameof(maximumLength));

            MaximumLength = maximumLength;
            MinimumLength = minimumLength;
            Lowercase = lowercase;
            Uppercase = uppercase;
            Number = number;
            Special = special;
        }

        // ----- Properties ----- //

        public static PasswordRequirements Default => defaults ?? (defaults = new PasswordRequirements());

        public int? MinimumLength { get; }

        public int MaximumLength { get; }

        public bool Lowercase { get; }

        public bool Uppercase { get; }

        public bool Number { get; }

        public bool Special { get; }
    }
}

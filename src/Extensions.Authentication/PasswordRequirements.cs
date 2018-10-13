namespace Extensions.Authentication
{
    public class PasswordRequirements
    {
        private static PasswordRequirements defaults = null;

        public PasswordRequirements(int maximumLength = 33, int minimumLength = 8, bool lowercase = true, bool uppercase = true, bool number = true, bool special = true)
        {
            MaximumLength = maximumLength;
            MinimumLength = minimumLength;
            Lowercase = lowercase;
            Uppercase = uppercase;
            Number = number;
            Special = special;
        }

        public static PasswordRequirements Defaults()
        {
            return defaults ?? (defaults = new PasswordRequirements());
        }

        // ----- Properties ----- //

        public int? MinimumLength { get; }

        public int MaximumLength { get; }

        public bool Lowercase { get; }

        public bool Uppercase { get; }

        public bool Number { get; }

        public bool Special { get; }
    }
}

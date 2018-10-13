using System;

namespace Extensions.Authentication.JwtBearer
{
    /// <summary>
    /// Defines a configuration for handling JSON Web Tokens.
    /// </summary>
    public class JwtConfiguration
    {
        /// <summary>Sets or gets the length of time an issued token should be valid.</summary>
        public TimeSpan Expiry { set; get; }

        /// <summary>
        /// Sets or gets the audience that issued tokens are applicable.
        /// This tends to be an identifier of an application that will use the token, normally formatted as a web URI.
        /// </summary>
        public string Audience { set; get; }

        /// <summary>
        /// Sets or gets the identifier of the issuer of the tokens.
        /// This tends to be an identifier of the application that is issuing the token, normally formatted as a web URI.
        /// </summary>
        public string Issuer { set; get; }
    }
}

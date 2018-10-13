using System;

namespace Extensions.AspNetCore.Authentication.JwtBearer
{
    public class JwtConfiguration
    {
        public TimeSpan Expiry { set; get; }

        public string Audience { set; get; }

        public string Issuer { set; get; }
    }
}

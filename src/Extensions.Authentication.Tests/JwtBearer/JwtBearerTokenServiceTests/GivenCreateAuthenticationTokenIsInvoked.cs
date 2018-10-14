using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Extensions.Authentication.JwtBearer.Tests
{
    [TestFixture]
    public class GivenCreateAuthenticationTokenIsInvoked
    {
        private byte[] key = null;
        private JwtBearerTokenService subject = null;
        private string result = null;

        [SetUp]
        public void Setup()
        {
            key = new HMACSHA256().Key;

            Mock<ISymmetricKeyProvider> keyProvider = new Mock<ISymmetricKeyProvider>();
            keyProvider
                .SetupGet(instance => instance.Key)
                .Returns(key);

            subject = new JwtBearerTokenService(keyProvider.Object, new JwtConfiguration
            {
                Audience = "https://some.audience.com",
                Expiry = TimeSpan.FromMilliseconds(150),
                Issuer = "https://some.issuer.com"
            });
            result = subject.CreateAuthenticationToken(new Claim(ClaimTypes.Email, "some@value.com"), new Claim(ClaimTypes.Gender, "Male"));
        }

        [Test]
        public void ThenTheResultIsAValidJwtToken()
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudience = "https://some.audience.com",
                ValidIssuer = "https://some.issuer.com",
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            new JwtSecurityTokenHandler().ValidateToken(result, validationParameters, out SecurityToken token);
        }

        [Test]
        public void ThenAnInvalidSigningKeyIsNotAValidJwtToken()
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudience = "https://some.audience.com",
                ValidIssuer = "https://some.issuer.com",
                IssuerSigningKey = new SymmetricSecurityKey(new HMACSHA256().Key),
            };
            Assert.Throws<SecurityTokenInvalidSignatureException>(() => new JwtSecurityTokenHandler().ValidateToken(result, validationParameters, out SecurityToken token));
        }

        [Test]
        public async Task ThenTheResultIsOnlyValidBeforeTheExpiry()
        {
            await Task.Delay(350);
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudience = "https://some.audience.com",
                ValidIssuer = "https://some.issuer.com",
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            Assert.Throws<SecurityTokenExpiredException>(() => new JwtSecurityTokenHandler().ValidateToken(result, validationParameters, out SecurityToken token));
        }

        [Test]
        public void ThenTheDecodedTokenClaimsAreMappedVerbatim()
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidAudience = "https://some.audience.com",
                ValidIssuer = "https://some.issuer.com",
                IssuerSigningKey = new SymmetricSecurityKey(key),
            };
            JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(result);

            Assert.AreEqual("some@value.com", token.Claims.First(x => x.Type == "email").Value);
            Assert.AreEqual("Male", token.Claims.First(x => x.Type == "gender").Value);
        }
    }
}

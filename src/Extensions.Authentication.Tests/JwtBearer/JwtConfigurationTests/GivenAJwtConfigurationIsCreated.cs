using NUnit.Framework;
using System;

namespace Extensions.Authentication.JwtBearer.Tests
{
    [TestFixture]
    public class GivenAJwtConfigurationIsCreated
    {
        private JwtConfiguration subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new JwtConfiguration
            {
                Audience = "audience",
                Expiry = TimeSpan.FromMinutes(5),
                Issuer = "issuer"
            };
        }

        [Test]
        public void ThenThePropertiesMapVerbatim()
        {
            Assert.AreEqual("audience", subject.Audience);
            Assert.AreEqual(TimeSpan.FromMinutes(5), subject.Expiry);
            Assert.AreEqual("issuer", subject.Issuer);
        }
    }
}

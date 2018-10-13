using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.Authentication.JwtBearer.Tests
{
    [TestFixture]
    public class GivenAddJwtIsInvokedWithInvalidParameters
    {
        [Test]
        public void WithoutAServiceCollection_ThenAnErrorIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => JwtExtensionsHelper.AddJwt(null, new JwtConfiguration()));
            Assert.AreEqual("services", exception.ParamName);
        }

        [Test]
        public void WithoutAConfiguration_ThenAnErrorIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => JwtExtensionsHelper.AddJwt(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("configuration", exception.ParamName);
        }
    }
}

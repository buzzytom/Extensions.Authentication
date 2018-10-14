using Extensions.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.Authentication.Tests
{
    [TestFixture]
    public class GivenAddJwtIsInvokedWithInvalidParameters
    {
        [Test]
        public void WithoutAServiceCollection_ThenAnErrorIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ExtensionsHelper.AddJwt(null, new JwtConfiguration()));
            Assert.AreEqual("services", exception.ParamName);
        }

        [Test]
        public void WithoutAConfiguration_ThenAnErrorIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ExtensionsHelper.AddJwt(Mock.Of<IServiceCollection>(), null));
            Assert.AreEqual("configuration", exception.ParamName);
        }
    }
}

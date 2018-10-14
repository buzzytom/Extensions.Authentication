using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;

namespace Extensions.Authentication.Tests.ExtensionsHelperTests
{
    [TestFixture]
    public class GivenAddTokenServiceIsInvoked
    {
        [Test]
        public void WithoutAServiceCollection_ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => ExtensionsHelper.AddTokenService(null));
            Assert.AreEqual("services", exception.ParamName);
        }

        [Test]
        public void WithAServiceCollection_ThenATokenServiceIsAdded()
        {
            Mock<IServiceCollection> services = new Mock<IServiceCollection>();

            ExtensionsHelper.AddTokenService(services.Object);

            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Scoped &&
                x.ServiceType == typeof(ITokenService) &&
                x.ImplementationType == typeof(TokenService))), Times.Once);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Extensions.Authentication.JwtBearer.Tests
{
    [TestFixture]
    public class GivenAddJwtIsInvoked
    {
        private Mock<IServiceCollection> services = null;
        private JwtConfiguration configuration = null;

        [SetUp]
        public void Setup()
        {
            List<ServiceDescriptor> descriptors = new List<ServiceDescriptor>();

            services = new Mock<IServiceCollection>();
            services
                .Setup(instance => instance.GetEnumerator())
                .Returns(descriptors.GetEnumerator());

            configuration = new JwtConfiguration
            {
                Audience = "some audience",
                Expiry = TimeSpan.FromDays(1),
                Issuer = "some issuer"
            };

            JwtExtensionsHelper.AddJwt(services.Object, configuration);
        }

        [Test]
        public void ThenTheConfigurationIsAddedAsAService()
        {
            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Singleton &&
                x.ServiceType == typeof(JwtConfiguration) &&
                x.ImplementationInstance == configuration)), Times.Once);
        }

        [Test]
        public void ThenTheSymmetricKeyProviderIsAddedAsAService()
        {
            services.Verify(instance => instance.Add(It.Is<ServiceDescriptor>(x =>
                x.Lifetime == ServiceLifetime.Singleton &&
                x.ServiceType == typeof(ISymmetricKeyProvider) &&
                x.ImplementationInstance.GetType() == typeof(SymmetricKeyProvider))), Times.Once);
        }
    }
}

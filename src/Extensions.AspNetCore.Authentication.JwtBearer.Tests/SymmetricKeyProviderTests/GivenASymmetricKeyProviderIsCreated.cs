using NUnit.Framework;

namespace Extensions.AspNetCore.Authentication.JwtBearer.Tests
{
    [TestFixture]
    public class GivenASymmetricKeyProviderInstanceIsCreated
    {
        private SymmetricKeyProvider subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new SymmetricKeyProvider();
        }

        [Test]
        public void ThenTheKeyPropertyIsInitialised()
        {
            Assert.IsTrue(subject.Key.Length > 0);
        }

        [Test]
        public void ThenTheKeyPropertyIsDistinct()
        {
            CollectionAssert.AreNotEqual(subject.Key, new SymmetricKeyProvider().Key);
        }
    }
}

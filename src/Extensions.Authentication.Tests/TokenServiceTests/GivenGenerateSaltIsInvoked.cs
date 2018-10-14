using NUnit.Framework;

namespace Extensions.Authentication.Tests.TokenServiceTests
{
    [TestFixture]
    public class GivenGenerateSaltIsInvoked
    {
        private TokenService subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new TokenService();
        }

        [Test]
        public void ThenTheGeneratedValueIsAReasonableLength()
        {
            string salt = subject.GenerateSalt();
            Assert.IsTrue(salt.Length >= 16 && salt.Length < 128);
        }

        [Test]
        public void ThenGeneratedSaltsAreDistinct()
        {
            string salt = subject.GenerateSalt();
            for (int i = 0; i < 100; i++)
                Assert.AreNotEqual(salt, subject.GenerateSalt());
        }

        [Test]
        public void ThenAllGeneratedSaltsAreTheSameLength()
        {
            string salt = subject.GenerateSalt();
            for (int i = 0; i < 100; i++)
                Assert.AreEqual(salt.Length, subject.GenerateSalt().Length);
        }
    }
}

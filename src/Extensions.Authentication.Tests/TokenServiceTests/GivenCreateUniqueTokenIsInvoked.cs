using NUnit.Framework;

namespace Extensions.Authentication.Tests.TokenServiceTests
{
    [TestFixture]
    public class GivenCreateUniqueTokenIsInvoked
    {
        private TokenService subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new TokenService();
        }

        [Test]
        public void ThenTheGeneratedTokenIsTheSpecifiedLength()
        {
            Assert.AreEqual(24, subject.CreateUniqueToken(24).Length);
        }

        [Test]
        public void ThenTheGeneratedTokensAreDistinct()
        {
            string token = subject.CreateUniqueToken(24);
            for (int i = 0; i < 100; i++)
                Assert.AreNotEqual(token, subject.CreateUniqueToken(24));
        }
    }
}

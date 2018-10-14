using NUnit.Framework;

namespace Extensions.Authentication.Tests
{
    [TestFixture]
    public class GivenHashIsInvoked
    {
        private TokenService subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new TokenService();
        }

        [Test]
        public void ThenTheSameParametersProduceTheSameHash()
        {
            string salt = subject.GenerateSalt();
            string hashA = subject.Hash("some value", salt);
            string hashB = subject.Hash("some value", salt);
            Assert.AreEqual(hashA, hashB);
        }

        [Test]
        public void ThenDifferentValuesProduceDistrinctResults()
        {
            string salt = subject.GenerateSalt();
            string hash = subject.Hash("some value", salt);
            for (int i = 0; i < 20; i++)
                Assert.AreNotEqual(hash, subject.Hash("some value " + i, salt));
        }

        [Test]
        public void ThenDifferentSaltsProduceDistrinctResults()
        {
            string salt = subject.GenerateSalt();
            string hash = subject.Hash("some value", salt);
            for (int i = 0; i < 20; i++)
                Assert.AreNotEqual(hash, subject.Hash("some value", subject.GenerateSalt()));
        }
    }
}

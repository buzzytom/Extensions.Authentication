using NUnit.Framework;

namespace Extensions.Authentication.Tests
{
    [TestFixture]
    public class GivenTheDefaultPropertyIsAccessed
    {
        [Test]
        public void ThenThePropertiesMapVerbatim()
        {
            PasswordRequirements subject = PasswordRequirements.Default;

            Assert.AreEqual(33, subject.MaximumLength);
            Assert.AreEqual(8, subject.MinimumLength);
            Assert.IsTrue(subject.Lowercase);
            Assert.IsTrue(subject.Uppercase);
            Assert.IsTrue(subject.Number);
            Assert.IsTrue(subject.Special);
        }

        [Test]
        public void ThenOnlyASingleInstanceIsAvailable()
        {
            Assert.AreSame(PasswordRequirements.Default, PasswordRequirements.Default);
        }
    }
}

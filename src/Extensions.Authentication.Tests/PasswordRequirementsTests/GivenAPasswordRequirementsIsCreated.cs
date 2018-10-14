using NUnit.Framework;
using System;

namespace Extensions.Authentication.Tests
{
    [TestFixture]
    public class GivenAPasswordRequirementsIsCreated
    {
        [Test]
        public void WithValidConstructorParameters_ThenThePropertiesMapVerbatim()
        {
            PasswordRequirements subject = new PasswordRequirements(42, 41, true, true, true, true);

            Assert.AreEqual(42, subject.MaximumLength);
            Assert.AreEqual(41, subject.MinimumLength);
            Assert.IsTrue(subject.Lowercase);
            Assert.IsTrue(subject.Uppercase);
            Assert.IsTrue(subject.Number);
            Assert.IsTrue(subject.Special);
        }

        [Test]
        public void WithAMinimumLengthLessThanZero_ThenAnExceptionIsThrown()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new PasswordRequirements(42, -1, true, true, true, true));
            Assert.AreEqual("minimumLength", exception.ParamName);
        }

        [Test]
        public void WithAMaximumLengthLessThanTheMinimumLength_ThenAnExceptionIsThrown()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new PasswordRequirements(9, 10, true, true, true, true));
            Assert.AreEqual("maximumLength", exception.ParamName);
        }

        [Test]
        public void WithAMaximumLengthLessThanOrEqualToZero_ThenAnExceptionIsThrown()
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() => new PasswordRequirements(0, 0, true, true, true, true));
            Assert.AreEqual("maximumLength", exception.ParamName);
        }
    }
}

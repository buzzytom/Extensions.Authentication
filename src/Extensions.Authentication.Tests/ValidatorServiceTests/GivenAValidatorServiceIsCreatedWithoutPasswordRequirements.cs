using NUnit.Framework;
using System;

namespace Extensions.Authentication.Tests
{
    [TestFixture]
    public class GivenAValidatorServiceIsCreatedWithoutPasswordRequirements
    {
        [Test]
        public void ThenAnExceptionIsThrown()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new ValidatorService(null));
            Assert.AreEqual("requirements", exception.ParamName);
        }
    }
}

using NUnit.Framework;
using System.Linq;

namespace Extensions.Authentication.Tests
{
    [TestFixture("     ", "An email address must be provided.")]
    [TestFixture("", "An email address must be provided.")]
    [TestFixture(null, "An email address must be provided.")]
    public class GivenAnEmailIsBeingValidated
    {
        private readonly string email;
        private readonly string message;
        private ValidatorService subject = null;

        public GivenAnEmailIsBeingValidated(string email, string message)
        {
            this.email = email;
            this.message = message;
        }

        [SetUp]
        public void Setup()
        {
            subject = new ValidatorService(PasswordRequirements.Default);
        }

        [Test]
        public void ThenTheResponseErrorsShouldMapVerbatim()
        {
            string error = subject.GetEmailErrors(email).Single();
            Assert.AreEqual(message, error);
        }
    }
}

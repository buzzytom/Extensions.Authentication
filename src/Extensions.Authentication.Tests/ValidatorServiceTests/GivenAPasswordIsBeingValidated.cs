using NUnit.Framework;
using System.Linq;

namespace Extensions.Authentication.Tests.ValidatorServiceTests
{
    [TestFixture]
    public class GivenAPasswordIsBeingValidated
    {
        private ValidatorService subject = null;

        [SetUp]
        public void Setup()
        {
            subject = new ValidatorService(new PasswordRequirements(10, 8));
        }

        [Test]
        public void WhenAnNullPasswordIsProvided_ThenTheResponseErrorsShouldMapVerbatim()
        {
            string error = subject.GetPasswordErrors(null).SingleOrDefault();
            Assert.AreEqual("A password must be specified.", error);
        }

        [Test]
        public void WhenAWhiteSpacePasswordIsProvided_ThenTheResponseErrorsShouldMapVerbatim()
        {
            string error = subject.GetPasswordErrors("    ").SingleOrDefault();
            Assert.AreEqual("A password must be specified.", error);
        }

        [Test]
        public void WhenAnEmptyStringPasswordIsProvided_ThenTheResponseErrorsShouldMapVerbatim()
        {
            string error = subject.GetPasswordErrors("").SingleOrDefault();
            Assert.AreEqual("A password must be specified.", error);
        }

        [Test]
        public void WithAValidPassword_ThenThereShouldBeAnAssociatedError()
        {
            Assert.IsNull(subject.GetPasswordErrors("Valid125@!").SingleOrDefault());
        }

        [Test]
        public void WithALengthLessThanEight_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("1234567").ToArray();
            CollectionAssert.Contains(errors, "Password must be 8 characters or longer.");
        }

        [Test]
        public void WithALengthMoreThanTen_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("12345678901").ToArray();
            CollectionAssert.Contains(errors, "Password must be no more than 10 characters long.");
        }

        [Test]
        public void WithNoLowerCaseLetters_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("ABCDEFGHI").ToArray();
            CollectionAssert.Contains(errors, "Missing lowercase characters (a-z).");
        }

        [Test]
        public void WithNoUpperCaseLetters_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("abcdefghi").ToArray();
            CollectionAssert.Contains(errors, "Missing uppercase characters (A-Z).");
        }

        [Test]
        public void WithNoNumbers_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("abcdefghi").ToArray();
            CollectionAssert.Contains(errors, "Missing numbers (0-9).");
        }

        [Test]
        public void WithNoSpecialCharacter_ThenThereShouldBeAnAssociatedError()
        {
            string[] errors = subject.GetPasswordErrors("abcdefghi").ToArray();
            CollectionAssert.Contains(errors, "Missing special characters (e.g. !@#$%^&* ).");
        }
    }
}

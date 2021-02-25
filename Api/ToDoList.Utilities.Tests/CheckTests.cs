using NUnit.Framework;

namespace ToDoList.Utilities.Tests
{
    public class CheckTests
    {
        [Test]
        public void NotNull_PropertyIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => Check.NotNull(null, "TestPropertyName"), Throws.ArgumentNullException);
        }

        [Test]
        public void NotNull_PropertyIsNotNull_DoesNotThrowException()
        {
            Assert.That(() => Check.NotNull(new object(), "TestPropertyName"), Throws.Nothing);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void NotNullOrWhiteSpace_PropertyIsNullOrWhiteSpace_ThrowsArgumentNullException(string prop)
        {
            Assert.That(() => Check.NotNullOrWhiteSpace(prop, "TestPropertyName"), Throws.ArgumentNullException);
        }

        [Test]
        public void NotNullOrWhiteSpace_PropertyIsNotNullOrWhiteSpace_DoesNotThrowException()
        {
            Assert.That(() => Check.NotNullOrWhiteSpace("Valid property", "TestPropertyName"), Throws.Nothing);
        }
    }
}

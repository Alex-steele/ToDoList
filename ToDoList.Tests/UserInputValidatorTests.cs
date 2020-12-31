//using NUnit.Framework;
//using ToDoList.Core.Validators;
//using ToDoList.Core.Validators.Enums;
//using ToDoList.Core.Validators.Interfaces;

//namespace ToDoList.Tests
//{
//    public class UserInputValidatorTests
//    {
//        private IAddCommandValidator sut;

//        [SetUp]
//        public void SetUp()
//        {
//            sut = new AddCommandValidator();
//        }

//        [Test]
//        [TestCase(null)]
//        [TestCase("")]
//        [TestCase(" ")]
//        public void Validate_InputIsNullOrWhiteSpace_ReturnsInvalid(string input)
//        {
//            // Act
//            var result = sut.Validate(input);

//            // Assert
//            Assert.That(result, Is.EqualTo(ValidationResult.Invalid));
//        }

//        [Test]
//        public void Validate_InputIsUnder200_ReturnsValid()
//        {
//            // Arrange
//            var testInput = new string('a', 199);

//            // Act
//            var result = sut.Validate(testInput);

//            // Assert
//            Assert.That(result, Is.EqualTo(ValidationResult.Valid));
//        }

//        [Test]
//        public void Validate_InputIs200_ReturnsValid()
//        {
//            // Arrange
//            var testInput = new string('a', 200);

//            // Act
//            var result = sut.Validate(testInput);

//            // Assert
//            Assert.That(result, Is.EqualTo(ValidationResult.Valid));
//        }

//        [Test]
//        public void Validate_InputIsOver200_ReturnsInvalid()
//        {
//            // Arrange
//            var testInput = new string('a', 201);

//            // Act
//            var result = sut.Validate(testInput);

//            // Assert
//            Assert.That(result, Is.EqualTo(ValidationResult.Invalid));
//        }
//    }
//}

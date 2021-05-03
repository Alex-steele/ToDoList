using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ToDoList.Core.Models;
using ToDoList.Core.Validators;
using ToDoList.Core.Validators.Interfaces;

namespace ToDoList.Core.Tests.Validators
{
    public class GetItemByValueQueryValidatorTests
    {
        private IGetItemByValueQueryValidator sut;

        [SetUp]
        public void SetUp()
        {
            sut = new GetItemByValueQueryValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Validate_ItemValueIsNullOrWhiteSpace_ReturnsCorrectError(string itemValue)
        {
            // Arrange
            var testModel = new GetItemByValueQueryModel()
            {
                ItemValue = itemValue
            };

            // Act
            var result = sut.Validate(testModel);

            // Assert
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Errors.Single().PropertyName, Is.EqualTo(nameof(testModel.ItemValue)));
            Assert.That(result.Errors.Single().ErrorMessage, Is.EqualTo("item value is required"));
        }
    }
}

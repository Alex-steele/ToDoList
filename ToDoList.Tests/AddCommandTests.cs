//using FakeItEasy;
//using NUnit.Framework;
//using System.Collections.Generic;
//using ToDoList.Core.Commands;
//using ToDoList.Core.Models;
//using ToDoList.Core.Validators.Enums;
//using ToDoList.Core.Validators.Interfaces;
//using ToDoList.Data.Entities;
//using ToDoList.Data.Repositories.Interfaces;

//namespace ToDoList.Tests
//{
//    [TestFixture]
//    public class AddCommandTests
//    {
//        private IToDoListRepository repository;
//        private IAddCommandValidator validator;
//        private AddCommand sut;

//        [SetUp]
//        public void SetUp()
//        {
//            repository = A.Fake<IToDoListRepository>();
//            validator = A.Fake<IAddCommandValidator>();

//            sut = new AddCommand(repository, validator);
//        }

//        [Test]
//        public void Execute_ModelIsInvalid_ItemIsNotAdded()
//        {
//            // Arrange
//            var testModel = new AddCommandModel
//            {
//                ItemValue = "Invalid Value"
//            };

//            A.CallTo(() => validator.Validate(testModel.ItemValue))
//                .Returns(ValidationResult.Invalid);

//            // Act
//            sut.Execute(testModel);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
//                .MustNotHaveHappened();
//        }

//        [Test]
//        public void Execute_GetAllReturnsNull_ItemIsNotAdded()
//        {
//            // Arrange
//            var testModel = new AddCommandModel
//            {
//                ItemValue = "Valid Value"
//            };

//            A.CallTo(() => validator.Validate(testModel.ItemValue))
//                .Returns(ValidationResult.Valid);

//            A.CallTo(() => repository.GetAll())
//                .Returns(null);

//            // Act
//            sut.Execute(testModel);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Value == testModel.ItemValue)))
//                .MustNotHaveHappened();
//        }

//        [Test]
//        public void Execute_GetAllReturnsEmpty_AddsItemWithId1()
//        {
//            // Arrange
//            var testModel = new AddCommandModel
//            {
//                ItemValue = "Valid Value"
//            };

//            A.CallTo(() => validator.Validate(testModel.ItemValue))
//                .Returns(ValidationResult.Valid);

//            A.CallTo(() => repository.GetAll())
//                .Returns(new List<ListItem>());

//            // Act
//            sut.Execute(testModel);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Id == 1 && x.Value == testModel.ItemValue)))
//                .MustHaveHappenedOnceExactly();
//        }

//        [Test]
//        public void Execute_GetAllReturnsList_AddsItemWithNextId()
//        {
//            // Arrange
//            var testModel = new AddCommandModel
//            {
//                ItemValue = "Valid Value"
//            };

//            A.CallTo(() => validator.Validate(testModel.ItemValue))
//                .Returns(ValidationResult.Valid);

//            A.CallTo(() => repository.GetAll())
//                .Returns(new List<ListItem>
//                {
//                    new ListItem
//                    {
//                        Id = 1
//                    }
//                });

//            // Act
//            sut.Execute(testModel);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Id == 2 && x.Value == testModel.ItemValue)))
//                .MustHaveHappenedOnceExactly();
//        }
//    }
//}
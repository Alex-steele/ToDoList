//using FakeItEasy;
//using NUnit.Framework;
//using System.Collections.Generic;
//using ToDoList.Core.Commands;
//using ToDoList.Data.Entities;
//using ToDoList.Data.Repositories.Interfaces;

//namespace ToDoList.Tests
//{
//    [TestFixture]
//    public class AddCommandTests
//    {
//        private IToDoListRepository repository;
//        private AddCommand sut;

//        [SetUp]
//        public void SetUp()
//        {
//            repository = A.Fake<IToDoListRepository>();
//            sut = new AddCommand(repository);
//        }

//        [Test]
//        public void AddItem_GetAllReturnsNull_ThrowsException()
//        {
//            // Arrange
//            const string itemValue = "Test Value";

//            A.CallTo(() => repository.GetAll())
//                .Returns(null);

//            // Act & Assert
//            Assert.That(() => sut.Execute(itemValue), Throws.Exception);
//        }

//        [Test]
//        public void AddItem_GetAllReturnsEmpty_AddsItemWithId1()
//        {
//            // Arrange
//            const string itemValue = "Test Value";

//            A.CallTo(() => repository.GetAll())
//                .Returns(new List<ListItem>());

//            // Act
//            sut.Execute(itemValue);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Id == 1 && x.Value == "Test Value")))
//                .MustHaveHappenedOnceExactly();
//        }

//        [Test]
//        public void AddItem_GetAllReturnsList_AddsItemWithNextId()
//        {
//            // Arrange
//            A.CallTo(() => repository.GetAll())
//                .Returns(new List<ListItem>
//                {
//                    new ListItem
//                    {
//                        Id = 1
//                    }
//                });

//            const string itemValue = "Test Value";

//            // Act
//            sut.Execute(itemValue);

//            // Assert
//            A.CallTo(() => repository.Add(A<ListItem>.That.Matches(x => x.Id == 2 && x.Value == "Test Value")))
//                .MustHaveHappenedOnceExactly();
//        }
//    }
//}
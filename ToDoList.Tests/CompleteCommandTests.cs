//using FakeItEasy;
//using NUnit.Framework;
//using ToDoList.Core.Commands;
//using ToDoList.Data.Entities;
//using ToDoList.Data.Repositories.Interfaces;

//namespace ToDoList.Tests
//{
//    [TestFixture]
//    public class CompleteCommandTests
//    {
//        private IToDoListRepository repository;
//        private CompleteCommand sut;

//        [SetUp]
//        public void SetUp()
//        {
//            repository = A.Fake<IToDoListRepository>();
//            sut = new CompleteCommand(repository);
//        }
//        [Test]
//        public void CompleteItem_InputIdMatchesItemId_ItemIsCompleted()
//        {
//            // Arrange
//            const int id = 1;

//            A.CallTo(() => repository.GetById(id)).Returns(new ListItem
//            {
//                Id = 1,
//                Completed = false
//            });

//            // Act
//            sut.Execute(id);

//            // Assert
//            A.CallTo(() => repository.Complete(A<ListItem>.That.Matches(x => x.Id == 1)))
//                .MustHaveHappenedOnceExactly();
//        }
//    }
//}

namespace ToDoList.Core.Commands.Interfaces
{
    public interface ICompleteCommand
    {
        void CompleteItem(int id);
    }
}
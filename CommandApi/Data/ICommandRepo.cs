using CommandApi.Models;

namespace CommandApi.App.Data
{
    public interface ICommandRepo
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int? id);
        void CreateCommand(Command command); 
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        void Save(); 
    }
}

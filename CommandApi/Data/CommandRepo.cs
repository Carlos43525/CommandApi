using CommandApi.Models;

namespace CommandApi.App.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Command> GetAllCommands() => _context.Commands.ToList();

        public Command GetCommandById(int? id) => _context.Commands.FirstOrDefault(p => p.Id == id);
        
        public void CreateCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _context.Commands.Add(command);
        }

        public void UpdateCommand(Command command)
        {
            // 
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            _context.Commands.Remove(command);
        }

        public void Save() => _context.SaveChanges();
    }
}

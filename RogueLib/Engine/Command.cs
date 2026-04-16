namespace RogueLib.Engine
{
    public class Command
    {
        private string _name;
        public string Name => _name;

        // Adding an optional Argument allows for more complex commands later
        public object? Argument { get; }

        public Command(string name, object? argument = null)
        {
            _name = name;
            Argument = argument;
        }
    }
}
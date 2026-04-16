using RogueLib.Dungeon;
using RogueLib.Utilities;
using CommandMap = System.Collections.Generic.Dictionary<System.ConsoleKey, string>;

namespace RogueLib.Engine
{
    public abstract class Scene : ICommandable, IDrawable
    {
        public abstract void DoCommand(Command command);
        public abstract void Draw(IRenderWindow disp);
        public abstract void Update();

        protected Player? _player;
        public Game? _game;
        protected bool _levelActive = true;

        public bool IsActive => _levelActive;
        protected CommandMap _commandMap;

        public bool HasCommand(ConsoleKey inputKey) => _commandMap.ContainsKey(inputKey);
        public string GetCommand(ConsoleKey inputKey) => _commandMap[inputKey];

        protected void RegisterCommand(ConsoleKey inputKey, string command)
            => _commandMap[inputKey] = command;

        public Scene() { _commandMap = new(); }
    }
}
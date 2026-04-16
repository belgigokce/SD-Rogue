using System;
using RogueLib.Dungeon;
using RogueLib.Utilities;

namespace RogueLib.Engine
{
    public class Game
    {
        public const int width = 78;
        public const int height = 25;

        protected Scene? _currentLevel;
        protected bool _isQuit;
        protected IRenderWindow? _window;
        protected Player? _player;

        public Game() { _isQuit = false; }

        public void run()
        {
            while (_currentLevel!.IsActive)
            {
                if (_window is null)
                    throw new Exception("Game window not initialized");

                _currentLevel!.Draw(_window);
                _window!.Display();
                HandleUserInput();
                _currentLevel!.Update();
            }
        }

        protected virtual void HandleUserInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (_currentLevel!.HasCommand(key.Key))
                _currentLevel!.DoCommand(new Command(_currentLevel!.GetCommand(key.Key)));
        }
    }
}
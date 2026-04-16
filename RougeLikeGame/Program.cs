using System;
using RogueLib.Engine;

namespace RlGameNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Game game = new MyGame();
            game.run();
        }
    }
}
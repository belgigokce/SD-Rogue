using System;
using RogueLib.Dungeon;
using RogueLib.Engine;
using RogueLib.Utilities;

namespace RlGameNS
{
    public class MyGame : Game
    {
        private void init()
        {
            _window = new ScreenBuff();
            _player = new Rogue();
            _currentLevel = new Level(_player, map1, this);

            // Welcome message via the Observer/Singleton UIManager
            UIManager.Instance.DisplayMessage("Welcome! Arrow/WASD to move | [Space] attack | [Q] quit");
        }

        public MyGame() { init(); }

        public const string map1 =
            """

                       ┌──────┐          ┌─────────────┐
                       │......│        ##+.............│            ┌───────┐
                       │......│        # │.............+##          │.......│
                       │......+######### └──────────+──┘ ###########+.......│
                       │......│                     #               └───────┘
                       └──+───┘                     #
                   ########                 #########
              ┌────+┐                     ┌─+───────┐              ┌──────────────────┐
              │.....│                     │.........│              │..................│
              │.....+#####################+.........│              │..................│
              │.....│                     │.........│              │..................│
              │.....│                     │.........│              │..................│
              │.....│                     │.........+##############+..................│
              └─+───┘                     └───+─────┘              └───────────────+──┘
                #                             #                                    #
                ######               ┌────────+──────────────┐                     #
                     #             ##+.......................|                     #
                     #             # |.......................|   ###################
                     #             # |.......................|   #
                     #             # |.......................+####
                     #             # └───────────────────────┘
                     ###############
                     
                     
            """;
    }
}
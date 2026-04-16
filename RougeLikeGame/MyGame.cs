using RogueLib.Dungeon;
using RogueLib.Engine;
using RogueLib.Utilities;

namespace RlGameNS;


public class MyGame : Game {
   
   private void init() {
      // To create a new game just need to 
      // 'inject' an IRenderWindow to draw the game one
      // 'inject' a Player, the player lives outside or the Scene's because the 
      // player visits all the scenes and takes their inventory with them. 
      // you must load the first leveel, and your level or your game must manage 
      // the level switching. 
      
      _window       = new ScreenBuff();
      _player       = new Rogue();
      _currentLevel = new Level(_player, map1, this);

        // Observer: UIManager is already a singleton wired up inside Level.
        // Demo: show player which strategy they're using at game start.
        UIManager.Instance.DisplayMessage("Welcome! [Space] to attack, [Q] to quit.");

    }

   public MyGame() {
      // init level on construction 
      init();
   }

   
   // ----------------------------------------------------------------
   // string to use as the backgound on our first level
   // ----------------------------------------------------------------

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
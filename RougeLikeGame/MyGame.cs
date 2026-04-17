using RogueLib.Dungeon;
using RogueLib.Engine;
using RogueLib.Utilities;

namespace RlGameNS;


public class MyGame : Game
{

    private List<string> _maps;                             // added by BG
    private int _currentMapIndex = 0;                       // added by BG

    private void init()
    {
        // To create a new game just need to 
        // 'inject' an IRenderWindow to draw the game one
        // 'inject' a Player, the player lives outside or the Scene's because the 
        // player visits all the scenes and takes their inventory with them. 
        // you must load the first leveel, and your level or your game must manage 
        // the level switching. 

        _window = new ScreenBuff();
        _player = new Player();


        //maps in order
        _maps = new List<string> { map1, map2, map3 };      // added by BG    
        _currentLevel = new Level(_player, _maps[_currentMapIndex], this);

    }

    // method for switching levels                          // added by BG
    public void NextLevel()
    {
        _currentMapIndex++;

        if (_currentMapIndex < _maps.Count)
        {
            // Clear the console entirely to remove the old map
            Console.Clear();
            _window = new ScreenBuff(); // reset front buffer so full redraw happens

            // Reset player position so they aren't standing on the old 'E' spot
            _player.Pos = new Vector2(4, 12);

            //create new level
            _currentLevel = new Level(_player, _maps[_currentMapIndex], this);

        }
        else
        {
            // No more maps left.
            Console.Clear();
            Console.WriteLine("You escaped the entire dungeon! YOU WIN!");
            Environment.Exit(0);
        }
    }





    public MyGame()
    {
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
               │......│        # │.............+##          │....E..│
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


    public const string map2 =
    """
                                                                                
      ┌──────────┐                     ┌──────────────┐                         
      │..........│                     │..............│                         
      │..........+#####################+..............│                         
      │..........│                     │..............│                         
      └────+─────┘                     └──────+───────┘                         
           #                                  #                                 
           #          ┌──────────┐            #            ┌─────────────┐      
    ┌──────+──────┐   │......E...│      ###################+.............│      
    │.............│   │..........+#######                  │.............│      
    │.............+###+..........│                         │.............│      
    │.............│   └──────────┘                         └─────────────┘      
    └──────+──────┘                                               #             
           #                                                      #             
           ##########          ┌────────────────┐          ########      
                    #          │................│          #                   
             ┌──────+──────┐   │................│   ┌──────+──────┐            
             │.............│   │................│   │.............│            
             │.............+###+................+###+.............│            
             │.............│   │................│   │.............│           
             └─────+───────┘   └────────────────┘   └──────+──────┘           
                   #                                       #                  
                   #########################################            
                                                                                
    """;

    public const string map3 =
    """
                                                                                
      ┌────────────┐                                       ┌────────────┐       
      │......E.....│                                       │............│       
      │............│                                       │............│       
      │............+#######################                │............│       
      │............│                #     #                │............│       
      └────────────┘                #     #                └──────+─────┘       
                                    #     #                       #             
                                    #  ┌──+──┐                    #             
             ########################  │.....│  ###################             
             #                         │.....│                                  
      ┌──────+─────┐                   │.....│                   ┌──────+─────┐ 
      │............│                   └──+──┘                   │............│ 
      │............+#############################################+............│ 
      │............│                   ┌──+──┐                   │............│ 
      └──────+─────┘                   │.....│                   └──────+─────┘ 
             #                         │.....│                          #       
             ########################  │.....│  #########################       
                                    #  └──+──┘  #                               
                                    #     #     #                               
      ┌────────────┐                #     #     #                 
      │............│                #     #######                
      │............+#################                           
      │............│                                           
      └────────────┘                                            
                                                                                
    """;


}


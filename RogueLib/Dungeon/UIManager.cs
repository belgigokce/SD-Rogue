using RogueLib.Utilities;

namespace RogueLib.Dungeon;

// Vans: New class — Singleton + Observer Pattern, watches game events and renders feedback messages on screen
public class UIManager : IObserver, IDrawable
{
    // Vans: Singleton instance
    private static UIManager? _instance;
    public static UIManager Instance => _instance ??= new UIManager();

    // Vans: Private constructor enforces Singleton
    private UIManager() { }

    private string _pendingMessage = string.Empty;

    // Vans: Tracks how many turns the current message stays visible
    private int _messageTurns = 0;

    // Vans: Reacts to game events broadcast by IObservable subjects
    public void Update(string subject, int value)
    {
        switch (subject)
        {
            case "enemyHit":
                SetMessage($"Enemy hits for {value} damage!");
                break;
            case "enemyHealth":
                SetMessage($"Enemy HP: {value}");
                break;
            case "enemyDied":
                SetMessage($"Enemy defeated! +{value} XP");
                break;
            default:
                SetMessage($"[{subject}: {value}]");
                break;
        }
    }

    // Vans: Allows Level to post a one-off message directly
    public void DisplayMessage(string message) => SetMessage(message);

    // Vans: Draws the pending message on row 23, clears it when timer expires
    public void Draw(IRenderWindow disp)
    {
        if (_messageTurns > 0)
        {
            disp.Draw(_pendingMessage.PadRight(78), new Vector2(0, 23), ConsoleColor.Yellow);
            _messageTurns--;
        }
        else
        {
            disp.Draw(new string(' ', 78), new Vector2(0, 23), ConsoleColor.Yellow);
        }
    }

    // Vans: Stores message and displays it for 3 turns
    private void SetMessage(string msg)
    {
        _pendingMessage = msg;
        _messageTurns = 3;
    }
}
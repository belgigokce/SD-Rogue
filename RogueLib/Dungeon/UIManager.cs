using RogueLib.Utilities;

namespace RogueLib.Dungeon;

// Vans: Observer Pattern — UIManager watches enemies and draws combat feedback.
// Vans: Singleton so Level and anything else always share the same instance.
public class UIManager : IObserver, IDrawable
{
    // Vans: Singleton instance
    private static UIManager? _instance;
    public static UIManager Instance => _instance ??= new UIManager();

    // Vans: Private constructor enforces Singleton
    private UIManager() { }

    // Vans: Pending message to render on the next Draw call
    private string _pendingMessage = string.Empty;

    // Vans: How many turns to keep the message visible
    private int _messageTurns = 0;

    // Vans: IObserver.Update — react to enemy events broadcast by IObservable
    public void Update(string subject, int value)
    {
        switch (subject)
        {
            // Vans: Enemy landed a hit on someone
            case "enemyHit":
                SetMessage($"Enemy hits for {value} damage!");
                break;

            // Vans: Enemy health changed — useful for a health bar later
            case "enemyHealth":
                SetMessage($"Enemy HP: {value}");
                break;

            // Vans: Enemy was killed, value = XP awarded
            case "enemyDied":
                SetMessage($"Enemy defeated! +{value} XP");
                break;

            default:
                SetMessage($"[{subject}: {value}]");
                break;
        }
    }

    // Vans: Called by Level to post a custom one-off message (e.g. "Nothing to attack")
    public void DisplayMessage(string message) => SetMessage(message);

    // Vans: IDrawable.Draw — render the pending message at the bottom of the screen
    public void Draw(IRenderWindow disp)
    {
        if (_messageTurns > 0)
        {
            // Vans: Row 23 sits just above the HUD row (24)
            disp.Draw(_pendingMessage.PadRight(78), new Vector2(0, 23), ConsoleColor.Yellow);
            _messageTurns--;
        }
        else
        {
            // Vans: Clear the message row when the timer expires
            disp.Draw(new string(' ', 78), new Vector2(0, 23), ConsoleColor.Yellow);
        }
    }

    // Vans: Store message and show it for 3 turns
    private void SetMessage(string msg)
    {
        _pendingMessage = msg;
        _messageTurns = 3;
    }
}
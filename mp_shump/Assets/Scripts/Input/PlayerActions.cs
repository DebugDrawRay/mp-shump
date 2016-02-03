using UnityEngine;
using System.Collections;
using InControl;

public class PlayerActions : PlayerActionSet
{
    public PlayerAction up;
    public PlayerAction down;
    public PlayerAction left;
    public PlayerAction right;
    public PlayerTwoAxisAction move;

    public PlayerAction primary;
    public PlayerAction secondary;
    public PlayerAction shield;

    public PlayerAction pause;
    public PlayerAction quit;
    public PlayerActions()
    {
        up = CreatePlayerAction("Move Up");
        down = CreatePlayerAction("Move Down");
        left = CreatePlayerAction("Move Left");
        right = CreatePlayerAction("Move Right");

        move = CreateTwoAxisPlayerAction(left, right, down, up);

        primary = CreatePlayerAction("Fire Primary");
        secondary = CreatePlayerAction("Fire Secondary");
        shield = CreatePlayerAction("Activate Shield");

        pause = CreatePlayerAction("Pause");
        quit = CreatePlayerAction("Quit");

    }

    public static PlayerActions BindActionsWithController()
    {
        PlayerActions actions = new PlayerActions();

        actions.up.AddDefaultBinding(InputControlType.LeftStickUp);
        actions.down.AddDefaultBinding(InputControlType.LeftStickDown);
        actions.left.AddDefaultBinding(InputControlType.LeftStickLeft);
        actions.right.AddDefaultBinding(InputControlType.LeftStickRight);

        actions.primary.AddDefaultBinding(InputControlType.Action1);
        actions.secondary.AddDefaultBinding(InputControlType.Action2);
        actions.shield.AddDefaultBinding(InputControlType.LeftBumper);

        actions.pause.AddDefaultBinding(InputControlType.Start);
        actions.quit.AddDefaultBinding(InputControlType.Back);

        return actions;
    }

    public static PlayerActions BindActionsWithKeyboard()
    {
        PlayerActions actions = new PlayerActions();

        actions.up.AddDefaultBinding(Key.W);
        actions.down.AddDefaultBinding(Key.S);
        actions.left.AddDefaultBinding(Key.A);
        actions.right.AddDefaultBinding(Key.D);

        actions.primary.AddDefaultBinding(Key.Space);
        actions.secondary.AddDefaultBinding(Key.RightShift);
        actions.shield.AddDefaultBinding(Key.Slash);

        actions.pause.AddDefaultBinding(Key.Escape);
        actions.quit.AddDefaultBinding(Key.F1);

        return actions;
    }
}

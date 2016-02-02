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

    public PlayerActions()
    {
        up = CreatePlayerAction("Move Up");
        down = CreatePlayerAction("Move Down");
        left = CreatePlayerAction("Move Left");
        right = CreatePlayerAction("Move Right");

        primary = CreatePlayerAction("Fire Primary");
        secondary = CreatePlayerAction("Fire Secondary");
        shield = CreatePlayerAction("Activate Shield");

        move = CreateTwoAxisPlayerAction(left, right, down, up);
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

        return actions;
    }
}

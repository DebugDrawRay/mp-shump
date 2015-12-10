using UnityEngine;
using System.Collections;

public class controllerListener : IinputListener
{
    private string player;

    public controllerListener(int playerNumber)
    {
        player = "_" + playerNumber.ToString();
    }
    public float horAxis()
    {
        return Input.GetAxisRaw(inputs.Horizontal + player);
    }
    public float verAxis()
    {
        return (Input.GetAxisRaw(inputs.Vertical + player))*-1;
    }
    public bool firePrimary()
    {
        return Input.GetButton(inputs.FirePrimary + player);
    }
    public bool fireSecondary()
    {
        return Input.GetButtonDown(inputs.FireSecondary + player);
    }
    public bool fireBomb()
    {
        return Input.GetButtonDown(inputs.FireBomb + player);
    }
    public bool raiseShields()
    {
        return Input.GetButton(inputs.RaiseShields + player);
    }
}

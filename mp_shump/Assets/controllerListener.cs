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
        return Input.GetAxis(inputs.Horizontal + player);
    }
    public float verAxis()
    {
        return (Input.GetAxis(inputs.Vertical + player))*-1;
    }
    public bool fireWeapon()
    {
        return Input.GetButtonDown(inputs.Fire + player);
    }
}

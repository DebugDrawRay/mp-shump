using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreFlightCheckController : MonoBehaviour
{
    public Image primaryCheck;
    public Image secondaryCheck;
    public Image moveCheck;
    public Image shieldCheck;
    public Image itemCheck;

    private Color activeColor = new Color(1, 1, 1, 1);

    public void activateButton(bool primary, bool secondary, bool move, bool shield, bool item)
    {
        if (primary)
        {
            primaryCheck.color = activeColor;
        }
        if (secondary)
        {
            secondaryCheck.color = activeColor;
        }
        if (move)
        {
            moveCheck.color = activeColor;
        }
        if (shield)
        {
            shieldCheck.color = activeColor;
        }
        if(item)
        {
            itemCheck.color = activeColor;
        }

        if (primaryCheck.color == activeColor &&
            secondaryCheck.color == activeColor &&
            moveCheck.color == activeColor &&
            shieldCheck.color == activeColor &&
            itemCheck.color == activeColor)
        {
            Destroy(gameObject);
        }

    }
}

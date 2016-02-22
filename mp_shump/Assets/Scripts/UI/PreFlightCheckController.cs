using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreFlightCheckController : MonoBehaviour
{
    public Image primaryCheck;
    public Image secondaryCheck;
    public Image moveCheck;
    public Image shieldCheck;

    private Color activeColor = new Color(1, 1, 1, 1);

    public void activateButton(bool primary, bool secondary, bool move, bool shield)
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

        if (primaryCheck.color == activeColor &&
            secondaryCheck.color == activeColor &&
            moveCheck.color == activeColor &&
            shieldCheck.color == activeColor)
        {
            Destroy(gameObject);
        }

    }
}

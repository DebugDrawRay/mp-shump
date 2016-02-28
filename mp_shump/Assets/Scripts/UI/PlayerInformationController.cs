using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInformationController : MonoBehaviour
{
    public Image itemIcon;
    public Image primaryIcon;
    public Image secondaryIcon;

    public Image[] livesIcons;

    public Sprite emptySprite;

    private int currentLives;

    public void SetItemIcon(Sprite icon)
    {
        itemIcon.sprite = VerifiedIcon(icon);
        Debug.Log("Items Set");
    }

    public void SetPrimaryIcon(Sprite icon)
    {
        primaryIcon.sprite = VerifiedIcon(icon);

        Debug.Log("Primary Set");
    }

    public void SetSecondaryIcon(Sprite icon)
    {
        secondaryIcon.sprite = VerifiedIcon(icon);

        Debug.Log("Secondary Set");
    }

    Sprite VerifiedIcon(Sprite icon)
    {
        if(icon == null)
        {
            return emptySprite;
        }
        else
        {
            return icon;
        }
    }
    public void ChangeCurrentLives(int amount)
    {
        currentLives = amount;

        for(int i = 0; i < livesIcons.Length - currentLives; i++)
        {
            livesIcons[i].enabled = true;
        }

        for(int i = currentLives; i < livesIcons.Length; i++)
        {
            livesIcons[i].enabled = false;
        }
        Debug.Log("Current Lives Set");
    }
}

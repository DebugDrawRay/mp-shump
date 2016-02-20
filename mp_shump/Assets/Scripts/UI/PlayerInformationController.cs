using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerInformationController : MonoBehaviour
{
    public Image itemIcon;
    public Image primaryIcon;
    public Image secondaryIcon;

    public Image[] livesIcons;

    private int currentLives;

    public void SetItemIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }

    public void SetPrimaryIcon(Sprite icon)
    {
        primaryIcon.sprite = icon;
    }

    public void SetSecondaryIcon(Sprite icon)
    {
        secondaryIcon.sprite = icon;
    }

    public void ChangeCurrentLives(int amount)
    {
        currentLives += amount;

        for(int i = 0; i < livesIcons.Length - currentLives; i++)
        {
            livesIcons[i].enabled = true;
        }

        for(int i = currentLives; i < livesIcons.Length; i++)
        {
            livesIcons[i].enabled = false;
        }
    }
}

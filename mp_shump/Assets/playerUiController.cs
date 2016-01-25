using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerUiController : MonoBehaviour
{
    public Vector2 centerOffset;

    public Text livesText;
    public Text bombsText;

    public Image primaryWeapon;
    public Image secondaryWeapon;

    public Image primaryWeaponMeter;
    public Image secondaryWeaponMeter;

    void Start()
    {
        transform.SetParent(gameCanvas.instance.gameObject.transform);
        GetComponent<RectTransform>().anchoredPosition = centerOffset;
    }
    public void updateLives(int lives)
    {
        livesText.text = lives.ToString();
    }
    public void updateBombs(int bombs)
    {
        bombsText.text = bombs.ToString();
    }
    public void updatePrimary(Sprite primary)
    {
        if (primary)
        {
            primaryWeapon.sprite = primary;
        }
    }
    public void updateSecondary(Sprite secondary)
    {
        if (secondary)
        {
            secondaryWeapon.sprite = secondary;
        }
    }
    public void updatePrimaryMeter(float current, float max)
    {
        primaryWeaponMeter.fillAmount = 1 - (current / max);
    }
    public void updateSecondaryMeter(float current, float max)
    {
        secondaryWeaponMeter.fillAmount = 1 - (current / max);
    }
}

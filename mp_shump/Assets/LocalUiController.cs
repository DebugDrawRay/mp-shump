using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocalUiController : MonoBehaviour
{
    public Image primaryWeaponMeter;
    public Image secondaryWeaponMeter;
    public Image shieldMeter;

    public void updatePrimaryMeter(float current, float max)
    {
        if (primaryWeaponMeter)
        {
            primaryWeaponMeter.fillAmount = 1 - (current / max);
        }
    }
    public void updateSecondaryMeter(float current, float max)
    {
        if (secondaryWeaponMeter)
        {
            secondaryWeaponMeter.fillAmount = 1 - (current / max);
        }
    }
    public void updateShieldMeter(float current, float max)
    {
        if (shieldMeter)
        {
            shieldMeter.fillAmount = 1 - (current / max);
        }
    }
}

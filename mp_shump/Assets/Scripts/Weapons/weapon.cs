using UnityEngine;
using System.Collections;

public interface weapon
{
    void setupWeapon();
    void updateWeapon();
    void fireWeapon();

    int currentWeaponLevel
    {
        get;
        set;
    }
    float currentResource
    {
        get;
        set;
    }

    float maxResource
    {
        get;
        set;
    }
}

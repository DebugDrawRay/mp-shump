using UnityEngine;
using System.Collections;

public class weaponController : actionController
{
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;

    public weapon currentPrimary
    {
        get;
        private set;
    }
    public weapon currentSecondary
    {
        get;
        private set;
    }

    public int weaponLevel;

    public int bombs;

    void Start()
    {
        if (primaryWeapon)
        {
            GameObject primary = Instantiate(primaryWeapon, transform.position, Quaternion.identity) as GameObject;
            primary.transform.SetParent(transform);
            currentPrimary = primary.GetComponent<weapon>();
            currentPrimary.setupWeapon();
        }
        if (secondaryWeapon)
        {
            GameObject secondary = Instantiate(secondaryWeapon, transform.position, Quaternion.identity) as GameObject;
            secondary.transform.SetParent(transform);
            currentSecondary = secondary.GetComponent<weapon>();
            currentSecondary.setupWeapon();
        }
    }
    void Update()
    {
        if (isActive)
        {
            if (currentPrimary != null)
            {
                currentPrimary.updateWeapon();
                if (input.firePrimary())
                {
                    currentPrimary.fireWeapon();
                }
            }
            if (currentSecondary != null)
            {
                currentSecondary.updateWeapon();
                if (input.fireSecondary())
                {
                    currentSecondary.fireWeapon();
                }
            }
        }
    }
}

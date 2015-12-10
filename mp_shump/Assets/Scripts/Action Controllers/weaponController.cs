using UnityEngine;
using System.Collections;

public class weaponController : actionController
{
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public GameObject bombWeapon;

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

    public int bombs;
    public int weaponLevel;

    private status currentStatus;
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

        currentStatus = GetComponent<status>();
    }
    void Update()
    {
        if(currentStatus)
        {
            weaponLevel = currentStatus.currentWeaponLevel;
        }
        if (isActive)
        {
            if (currentPrimary != null)
            {
                currentPrimary.currentWeaponLevel = weaponLevel;
                currentPrimary.updateWeapon();
                if (input.firePrimary())
                {
                    currentPrimary.fireWeapon();
                }
            }
            if (currentSecondary != null)
            {
                currentSecondary.currentWeaponLevel = weaponLevel;
                currentSecondary.updateWeapon();
                if (input.fireSecondary())
                {
                    currentSecondary.fireWeapon();
                }
            }
            if (bombWeapon != null)
            {
                if (input.fireBomb())
                {
                    GameObject bomb = Instantiate(bombWeapon, transform.position, Quaternion.identity) as GameObject;
                    bomb.transform.SetParent(transform);
                    bomb.tag = tag;
                }
            }
        }
    }
}

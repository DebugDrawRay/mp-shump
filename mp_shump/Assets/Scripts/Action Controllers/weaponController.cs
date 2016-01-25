using UnityEngine;

public class weaponController : actionController, IUiBroadcast
{
    [Header("Weapons")]
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
    public weapon currentBomb
    {
        get;
        private set;
    }

    public int weaponLevel;

    public playerUiController targetUI
    {
        get;
        set;
    }

    void Start()
    {
        if (primaryWeapon)
        {
            GameObject primary = Instantiate(primaryWeapon, transform.position, Quaternion.identity) as GameObject;
            primary.transform.SetParent(transform);
            primary.transform.rotation = primary.transform.parent.rotation;

            currentPrimary = primary.GetComponent<weapon>();
            targetUI.updatePrimary(currentPrimary.currentIcon);
        }
        if (secondaryWeapon)
        {
            GameObject secondary = Instantiate(secondaryWeapon, transform.position, Quaternion.identity) as GameObject;
            secondary.transform.SetParent(transform);
            secondary.transform.rotation = secondary.transform.parent.rotation;

            currentSecondary = secondary.GetComponent<weapon>();
            targetUI.updateSecondary(currentSecondary.currentIcon);

        }
        if (bombWeapon)
        {
            GameObject bomb = Instantiate(bombWeapon, transform.position, Quaternion.identity) as GameObject;
            bomb.transform.SetParent(transform);
            bomb.transform.rotation = bomb.transform.parent.rotation;

            currentBomb = bomb.GetComponent<weapon>();
            int currentAmmo = Mathf.RoundToInt(currentBomb.currentAmmo);
            targetUI.updateBombs(currentAmmo);
        }

    }
    void Update()
    {
        if (isActive)
        {
            if (currentPrimary != null)
            {
                currentPrimary.fireWeapon(input.firePrimary());
                targetUI.updatePrimary(currentPrimary.currentIcon);
                targetUI.updatePrimaryMeter(currentPrimary.currentAmmo, currentPrimary.maxAmmo);
            }
            if (currentSecondary != null)
            {
                currentSecondary.fireWeapon(input.fireSecondary());
                targetUI.updateSecondary(currentSecondary.currentIcon);
                targetUI.updateSecondaryMeter(currentSecondary.currentAmmo, currentSecondary.maxAmmo);
            }
            if (currentBomb != null)
            {
                currentBomb.fireWeapon(input.fireBomb());
                int currentAmmo = Mathf.RoundToInt(currentBomb.currentAmmo);
                targetUI.updateBombs(currentAmmo);
            }
        }
    }
}
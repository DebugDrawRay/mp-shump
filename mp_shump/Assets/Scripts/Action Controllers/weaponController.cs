using UnityEngine;

public class weaponController : actionController, IUiBroadcast
{
    [Header("Weapons")]
    public GameObject[] primaryWeapons;
    public GameObject[] secondaryWeapons;
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
    private int currentWeaponLevel;

    public playerUiController targetUI
    {
        get;
        set;
    }

    void Awake()
    {
        currentWeaponLevel = weaponLevel;
    }

    void Start()
    {
        updateCurrentWeapons(0);
    }

    public void updateCurrentWeapons(int levelIncrease)
    {
        currentWeaponLevel += levelIncrease;

        if (primaryWeapons.Length > 0 && currentWeaponLevel < primaryWeapons.Length)
        {
            GameObject primary = Instantiate(primaryWeapons[currentWeaponLevel], transform.position, Quaternion.identity) as GameObject;
            if (primary)
            {
                primary.transform.SetParent(transform);
                primary.transform.rotation = primary.transform.parent.rotation;

                currentPrimary = primary.GetComponent<weapon>();
                if (targetUI)
                {
                    targetUI.updatePrimary(currentPrimary.projectileIcon);
                }
            }
        }
        if (secondaryWeapons.Length > 0 && currentWeaponLevel < primaryWeapons.Length)
        {
            GameObject secondary = Instantiate(secondaryWeapons[currentWeaponLevel], transform.position, Quaternion.identity) as GameObject;
            if (secondary)
            {
                secondary.transform.SetParent(transform);
                secondary.transform.rotation = secondary.transform.parent.rotation;

                currentSecondary = secondary.GetComponent<weapon>();
                if (targetUI)
                {
                    targetUI.updateSecondary(currentSecondary.projectileIcon);
                }
            }

        }
        if (bombWeapon)
        {
            GameObject bomb = Instantiate(bombWeapon, transform.position, Quaternion.identity) as GameObject;
            bomb.transform.SetParent(transform);
            bomb.transform.rotation = bomb.transform.parent.rotation;

            currentBomb = bomb.GetComponent<weapon>();
            int currentAmmo = Mathf.RoundToInt(currentBomb.currentAmmo);
            if (targetUI)
            {
                targetUI.updateBombs(currentAmmo);
            }
        }

    }

    void Update()
    {
        if (isActive)
        {
            if (currentPrimary != null)
            {
                currentPrimary.fireWeapon(input.firePrimary());
                if (targetUI)
                {
                    if (currentPrimary.canFire)
                    {
                        targetUI.updatePrimaryMeter(currentPrimary.currentAmmo, currentPrimary.maxAmmo);
                    }
                }
            }
            if (currentSecondary != null)
            {
                currentSecondary.fireWeapon(input.fireSecondary());
                if (targetUI)
                {
                    if (currentPrimary.canFire)
                    {
                        targetUI.updateSecondaryMeter(currentSecondary.currentAmmo, currentSecondary.maxAmmo);
                    }
                }
            }
            if (currentBomb != null)
            {
                currentBomb.fireWeapon(input.fireBomb());
                if (targetUI)
                {
                    int currentAmmo = Mathf.RoundToInt(currentBomb.currentAmmo);
                    targetUI.updateBombs(currentAmmo);
                }
            }
        }
    }
}
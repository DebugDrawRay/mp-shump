using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour
{
    [Header("Projectiles")]
    public GameObject[] availableProjectiles;
    public Sprite[] projectileIcons;
    public Sprite currentIcon
    {
        get;
        private set;
    }

    [Range(0,3)]
    public int weaponLevel;

    public GameObject muzzleFlash;

    [Header("Properties")]
    public Vector2 originOffset;
    public float fireDelay;
    private float currentFireDelay;

    [Header("Reload and Recovery")]
    public float maxAmmo;
    public float currentAmmo
    {
        get;
        private set;
    }
    public float ammoUse;
    public float passiveAmmoRegen;
    public float reloadTime;
    public bool finiteAmmo;

    private float currentReloadTime;
    private bool canFire = true;

    //broadcast interface
    public playerUiController targetUI
    {
        get;
        set;
    }
    void Awake()
    {
        currentAmmo = maxAmmo;
        currentReloadTime = reloadTime;
        if (projectileIcons.Length >= 1)
        {
            currentIcon = projectileIcons[0];
        }
    }

    void Update()
    {
        ammoController();
        currentFireDelay -= Time.deltaTime;
    }
    
    public void fireWeapon(bool input)
    {
        if(input && canFire && currentFireDelay <= 0)
        {
            Vector2 fuckingDumbThing = transform.position;
            Vector2 adjOffset = originOffset;
            adjOffset.x = originOffset.x * transform.right.x;
            Vector2 offset = fuckingDumbThing + adjOffset;
            GameObject newProj = Instantiate(availableProjectiles[weaponLevel], offset, transform.rotation) as GameObject;
            newProj.tag = transform.parent.tag;
            if(muzzleFlash)
            {
                Instantiate(muzzleFlash, offset, transform.rotation);
            }
            currentFireDelay = fireDelay;
            currentAmmo -= ammoUse;
        }
    }

    void ammoController()
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo += passiveAmmoRegen;
        }

        if (currentAmmo <= 0 && canFire == true)
        {
            canFire = false;
        }

        if(!canFire && !finiteAmmo)
        {
            currentReloadTime -= Time.deltaTime;
            Debug.Log(currentReloadTime);
            if(currentReloadTime <= 0)
            {
                canFire = true;
                currentAmmo = maxAmmo;
                currentReloadTime = reloadTime;
            }
        }
    }
}

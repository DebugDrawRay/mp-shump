using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour
{
    [Header("Projectiles")]
    public GameObject projectile;
    public Sprite projectileIcon;

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
    public bool canFire
    {
        get;
        private set;
    }

    public playerUiController targetUI
    {
        get;
        set;
    }

    [Header("Events")]
    public bool broadcastFireEvent = false;

    public delegate void FireEvent();
    public static event FireEvent firedWeapon;

    void Awake()
    {
        canFire = true;
        currentAmmo = maxAmmo;
        currentReloadTime = reloadTime;
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
            if (firedWeapon != null && broadcastFireEvent)
            {
                firedWeapon();
            }
            Vector2 fuckingDumbThing = transform.position;
            Vector2 adjOffset = originOffset;
            adjOffset.x = originOffset.x * transform.right.x;
            Vector2 offset = fuckingDumbThing + adjOffset;
            GameObject newProj = Instantiate(projectile, offset, transform.rotation) as GameObject;
            newProj.tag = transform.parent.tag;
            if(muzzleFlash)
            {
                GameObject flash = Instantiate(muzzleFlash, offset, transform.rotation) as GameObject;
                flash.transform.SetParent(transform.parent);
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

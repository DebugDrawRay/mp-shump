using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour
{
    [Header("Projectiles")]
    public GameObject[] availableProjectiles;

    [Range(0,3)]
    public int weaponLevel;

    public GameObject muzzleFlash;

    [Header("Properties")]
    public Vector2 originOffset;
    public float fireDelay;
    private float currentFireDelay;

    [Header("Reload and Recovery")]
    public float maxAmmo;
    private float currentAmmo;

    public float passiveAmmoRegen;

    public float reloadTime;
    private float currentReloadTime;
    private bool canFire = true;

    //parent properties
    private Vector2 parentVelocity;
    private Vector2 lastPosition;

    void Awake()
    {
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
        }
    }

    void ammoController()
    {
        currentAmmo += passiveAmmoRegen;

        if(currentAmmo <= 0)
        {
            canFire = false;
            currentReloadTime -= Time.deltaTime;
            if(currentReloadTime <= 0)
            {
                canFire = true;
                currentAmmo = maxAmmo;
                currentReloadTime = reloadTime;
            }
        }
    }
}

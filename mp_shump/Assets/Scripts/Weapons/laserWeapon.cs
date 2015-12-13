using UnityEngine;
using System.Collections;

public class laserWeapon : MonoBehaviour, weapon
{
    [Header("Projectiles")]
    public GameObject[] projectileLevels;
    private GameObject currentLevel;

    public GameObject muzzleFlash;

    [Header("Properties")]
    public Vector2 laserOffset;
    public float[] fireDelayLevels;

    private float fireDelayLevel;
    private float currentFireDelay;

    public float laserHeatIncrease;
    public float laserHeatCooldown;
    public float laserCooldownBonus;
    public float laserHeatTresh;
    public float laserRecoveryFactor;

    public float currentLaserHeat;
    private bool isCool = true;

    private Vector2 parentVelocity;
    private Vector2 lastPosition;

    public float currentResource
    {
        get;
        set;
    }

    public float maxResource
    {
        get;
        set;
    }

    public int currentWeaponLevel
    {
        get;
        set;
    }

    public void setupWeapon()
    {
        fireDelayLevel = fireDelayLevels[0];
        currentFireDelay = fireDelayLevel;
        updateWeaponLevel();
    }

    public void updateWeapon()
    {
        currentResource = currentLaserHeat;
        maxResource = laserHeatTresh + (laserHeatTresh / laserRecoveryFactor);

        checkIfCool();

        if (currentFireDelay > 0)
        {
            currentFireDelay -= Time.deltaTime;
        }

        parentVelocity = getVelocity();
        updateWeaponLevel();
    }

    void updateWeaponLevel()
    {
        if(currentWeaponLevel <= projectileLevels.Length - 1)
        {
            currentLevel = projectileLevels[currentWeaponLevel];
            fireDelayLevel = fireDelayLevels[currentWeaponLevel];
        }
    }

    Vector2 getVelocity()
    {
        Vector2 newVel = Vector2.zero;
        Vector2 currentPosition = transform.root.position;
        if (lastPosition != currentPosition)
        {
            newVel = (lastPosition - currentPosition) / Time.deltaTime;
            lastPosition = currentPosition;
        }
        return newVel;
    }

    void checkIfCool()
    {
        if (currentLaserHeat < 0)
        {
            currentLaserHeat = 0;
        }
        else if (currentLaserHeat >= laserHeatTresh)
        {
            if (isCool)
            {
                currentLaserHeat = laserHeatTresh + (laserHeatTresh / laserRecoveryFactor);
                isCool = false;
            }
            else
            {
                currentLaserHeat -= laserHeatCooldown;
            }
        }
        else
        {
            currentLaserHeat -= laserHeatCooldown * laserCooldownBonus;
            isCool = true;
        }
    }

    public void fireWeapon()
    {
        if (currentFireDelay <= 0)
        {
            if (isCool)
            {
                currentLaserHeat += laserHeatIncrease;

                Vector2 origin = transform.parent.position;
                Vector2 dir = transform.parent.right;
                dir.y = laserOffset.y;
                dir.x = dir.x / laserOffset.x;
                Vector2 spawnPos = origin + dir;

                Instantiate(currentLevel).GetComponent<projectile>().Init(transform.parent, parentVelocity);
                /*GameObject proj = Instantiate(currentLevel, spawnPos, Quaternion.identity) as GameObject;
                proj.transform.SetParent(transform.parent);
                proj.tag = transform.parent.tag;
                proj.GetComponent<Rigidbody2D>().velocity = parentVelocity;*/  

                if (muzzleFlash)
                {
                    GameObject anim = Instantiate(muzzleFlash, spawnPos, Quaternion.identity) as GameObject;
                    anim.transform.SetParent(transform.parent);
                }
                currentFireDelay = fireDelayLevel;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class laserController : actionController
{
    public GameObject standardLaser;
    public Vector2 laserOffset;

    public int bombs;

    public int weaponLevel;
    public int missileLevel;

    public GameObject muzzleFlash;

    void Update()
    {
        firingHandler(input.firePrimary());
    }

    void firingHandler(bool fireWeapon)
    {
        if(fireWeapon)
        {
            Vector2 origin = transform.position;
            Vector2 spawnPos = origin + laserOffset;
            GameObject proj = Instantiate(standardLaser, spawnPos, facingRotation) as GameObject;
            proj.GetComponent<projectile>().direction = facingDirection;

            proj.GetComponent<interactionSource>().parent = parentObject;
            if (muzzleFlash)
            {
                GameObject anim = Instantiate(muzzleFlash, spawnPos, Quaternion.identity) as GameObject;
                anim.transform.SetParent(this.transform);
            }
        }
    }
}

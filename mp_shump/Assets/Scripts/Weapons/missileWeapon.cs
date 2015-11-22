using UnityEngine;
using System.Collections;

public class missileWeapon : MonoBehaviour, weapon
{
    [Header("Prefabs")]
    public GameObject missileObject;

    [Header("Launch Properties")]
    public Vector2[] launchDirections;
    public int waves;
    public float waveDelay;

    public float launchRotation = 45;
    public float launchSpeed;
    public float launchAccel;

    [Header("Missile Resources Control")]
    public float fireDelay;
    private float maxFireDelay;

    public int maxAmmo;
    public int currentAmmo
    {
        get;
        private set;
    }
    public float reloadInterval;
    private float currentReloadInterval;

    public int missileLevel;

    private bool hasAmmo;

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

    public void setupWeapon()
    {
        maxFireDelay = fireDelay;
        fireDelay = 0;
        currentAmmo = maxAmmo;
        currentReloadInterval = reloadInterval;
    }

    public void updateWeapon()
    {
        currentResource = currentAmmo;
        maxResource = maxAmmo;

        checkAmmo();
        if (fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
    }

    public void fireWeapon()
    {
        if (fireDelay <= 0)
        {
            if (hasAmmo)
            {
                StartCoroutine(launchWaves());
                currentAmmo--;
                currentReloadInterval = reloadInterval;

                fireDelay = maxFireDelay;
            }
        }
    }

    void checkAmmo()
    {
        currentReloadInterval -= Time.deltaTime;
        if (currentAmmo <= 0)
        {
            if (currentReloadInterval <= 0)
            {
                currentReloadInterval = reloadInterval;
                currentAmmo = maxAmmo;
            }
            hasAmmo = false;
        }
        else
        {
            hasAmmo = true;
        }
    }

    IEnumerator launchWaves()
    {
        for(int i = 1; i <= waves; i++)
        {
            launchMissiles();
            yield return new WaitForSeconds(waveDelay);
        }
    }
    void launchMissiles()
    {
        float mod = 1;
        for (int i = 0; i <= launchDirections.Length - 1; i++)
        {
            GameObject newMissile = Instantiate(missileObject, transform.position, Quaternion.identity) as GameObject;
            newMissile.transform.SetParent(transform);
            newMissile.tag = transform.parent.tag;

            float yRot = transform.parent.rotation.eulerAngles.y;
            Quaternion initRot = Quaternion.Euler(0, yRot, launchRotation * mod);
            newMissile.transform.rotation = initRot;
            newMissile.GetComponent<Rigidbody2D>().AddForce(launchDirections[i] * launchSpeed);
            mod = -mod;
        }
    }
}

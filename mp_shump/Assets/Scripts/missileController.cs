using UnityEngine;
using System.Collections;

public class missileController : actionController
{
    public GameObject missile;

    [Header("Launch Properties")]
    public float missileCount;
    public Vector2 launchDirection;
    public float launchRotation = 45;
    public float launchSpeed;
    public float launchAccel;

    public float fireDelay;
    private float maxFireDelay;

    void Start()
    {
        maxFireDelay = fireDelay;
        fireDelay = 0;
    }

    void Update()
    {
        if (isActive)
        {
            launchMissiles(input.fireSecondary());
        }
    }

    void launchMissiles(bool fireMissile)
    {
        if (fireDelay <= 0)
        {
            if (fireMissile)
            {
                for (int i = 1; i <= missileCount; i++)
                {
                    GameObject newMissile = Instantiate(missile, transform.position, facingRotation) as GameObject;
                    newMissile.transform.rotation = Quaternion.AngleAxis(launchRotation, Vector3.forward);
                    missile missileProp = newMissile.GetComponent<missile>();

                    newMissile.GetComponent<Rigidbody2D>().AddForce(launchDirection * launchSpeed);
                    missileProp.enemyObject = enemyObject;

                    launchRotation = -launchRotation;
                    launchDirection = -launchDirection;

                    newMissile.GetComponent<interactionSource>().parent = parentObject;
                }
                fireDelay = maxFireDelay;
            }
        }
        if(fireDelay > 0)
        {
            fireDelay -= Time.deltaTime;
        }
    }
}

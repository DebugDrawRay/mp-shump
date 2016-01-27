using UnityEngine;
using System.Collections;

public class shieldController : actionController 
{
    public GameObject shieldObject;
    private GameObject currentShield;

    [Header("Properties")]
    public float maxLifetime;
    public float currentLifetime
    {
        get;
        private set;
    }
    public float regenSpeed;

    private bool depleted;

    void Awake()
    {
        currentLifetime = maxLifetime;
    }

    void Update()
    {
        if (isActive)
        {
            raiseShields(input.raiseShields());
        }
        if(currentShield)
        {
            currentShield.transform.position = transform.position + currentShield.transform.right;
        }
    }

    void raiseShields(bool raise)
    {
        if (depleted)
        {
            currentLifetime += regenSpeed;
            if (currentLifetime >= maxLifetime)
            {
                depleted = false;
            }
        }
        else
        {
            if (raise && currentLifetime > 0 && !currentShield)
            {
                currentShield = Instantiate(shieldObject) as GameObject;
                currentShield.transform.rotation = transform.rotation;
                currentShield.transform.position = transform.position + currentShield.transform.right;
            }

            if (currentShield)
            {
                currentLifetime -= Time.deltaTime;
                if (currentLifetime <= 0)
                {
                    Destroy(currentShield);
                    currentShield = null;
                    depleted = true;
                }
            }
        }
    }
}

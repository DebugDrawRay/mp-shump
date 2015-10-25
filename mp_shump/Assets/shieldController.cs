using UnityEngine;
using System.Collections;

public class shieldController : actionController 
{
    public GameObject shieldObject;
    private GameObject currentShield;

    public float power;
    public float maxPower;

    void Awake()
    {
        maxPower = power;
    }
    void Update()
    {
        raiseShields(input.raiseShields());
    }

    void raiseShields(bool raise)
    {
        if(raise)
        {
            if(power > 0)
            {
                if (!currentShield)
                {
                    currentShield = Instantiate(shieldObject) as GameObject;
                }
                currentShield.transform.rotation = facingRotation;
                currentShield.transform.position = transform.position + (Vector3.right * facingDirection);
                power--;
            }
            else
            {
                if(currentShield)
                {
                    Destroy(currentShield);
                    currentShield = null;
                }
            }
        }
        else
        {
            if (currentShield)
            {
                Destroy(currentShield);
                currentShield = null;
            }
            if (power < maxPower)
            {
                power++;
            }
        }
    }
}

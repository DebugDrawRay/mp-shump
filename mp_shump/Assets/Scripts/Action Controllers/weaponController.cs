using UnityEngine;

public class weaponController : actionController
{
    [Header("Weapons")]
    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
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

    void Start()
    {
        if (primaryWeapon)
        {
            GameObject primary = Instantiate(primaryWeapon, transform.position, Quaternion.identity) as GameObject;
            primary.transform.SetParent(transform);
            primary.transform.rotation = primary.transform.parent.rotation;

            currentPrimary = primary.GetComponent<weapon>();
        }
        if (secondaryWeapon)
        {
            GameObject secondary = Instantiate(secondaryWeapon, transform.position, Quaternion.identity) as GameObject;
            secondary.transform.SetParent(transform);
            secondary.transform.rotation = secondary.transform.parent.rotation;

            currentSecondary = secondary.GetComponent<weapon>();
        }

    }
    void Update()
    {
        if (isActive)
        {
            if (currentPrimary != null)
            {
                currentPrimary.fireWeapon(input.firePrimary());
            }
            if (currentSecondary != null)
            {
                currentSecondary.fireWeapon(input.fireSecondary());
            }
            if (currentBomb != null)
            {
                if (input.fireBomb())
                {
                    GameObject bomb = Instantiate(bombWeapon, transform.position, Quaternion.identity) as GameObject;
                    bomb.transform.SetParent(transform);
                    bomb.tag = tag;
                }
            }
        }
    }
}
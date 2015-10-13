using UnityEngine;
using System.Collections;

public class weapons : MonoBehaviour
{
    public GameObject standardLaser;
    private controllerListener input;

    void Start()
    {
        input = new controllerListener(GetComponent<playerShip>().playerNumber);
    }
    void Update()
    {
        firingHandler(input.fireWeapon());
    }
    void firingHandler(bool fireWeapon)
    {
        if(fireWeapon)
        {
            GameObject proj = Instantiate(standardLaser, transform.position, Quaternion.identity) as GameObject;
            proj.GetComponent<projectile>().playerNumber = GetComponent<playerShip>().playerNumber;
        }
    }
}

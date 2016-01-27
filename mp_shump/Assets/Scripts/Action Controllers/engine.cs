using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class engine : actionController
{
    [Header("Movement Values")]
    public float moveSpeed;

    public float weaponUseStop;
    private float currentUseStop;

    private Rigidbody2D rigid;

    void Start()
    {
        weapon.firedWeapon += stopMovement;
    }

    void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (isActive)
        {
            movementHandler(input.horAxis(), input.verAxis());
        }
        else
        {
            movementHandler(0, 0);
        }
    }

    void stopMovement()
    {
        currentUseStop = weaponUseStop;
        Debug.Log("Stopped");
    }
    void movementHandler(float horAxis, float verAxis)
    {
        if(currentUseStop > 0)
        {
            currentUseStop -= 1;
            horAxis = 0;
            verAxis = 0;
        }
            Vector2 right = transform.root.right * horAxis * moveSpeed;
            Vector2 up = transform.up * verAxis * moveSpeed;
            Vector2 newVel = right + up;
            rigid.velocity = newVel;
    }
}

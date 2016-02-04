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

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        weapon.firedWeapon += stopMovement;
    }

    void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (isActive)
        {
            movementHandler(input.move.X, input.move.Y);
        }
        else
        {
            movementHandler(0, 0);
        }
        
        if(anim && input != null)
        {
            animateMovement();
        }
    }

    void animateMovement()
    {
        anim.SetFloat("movementDirection", input.move.Y);
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

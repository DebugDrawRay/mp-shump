using UnityEngine;
using System.Collections;

public class engine : actionController
{
    [Header("Movement Values")]
    public float moveSpeed;

    private Rigidbody2D rigid;
    void Update()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (isActive)
        {
            movementHandler(input.horAxis(), input.verAxis());
        }
    }

    void movementHandler(float horAxis, float verAxis)
    {
        Vector2 right = transform.right  * horAxis * moveSpeed;
        Vector2 up = transform.up * verAxis * moveSpeed;
        Vector2 newVel = right + up;
        rigid.velocity = newVel;
    }
}

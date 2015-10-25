using UnityEngine;
using System.Collections;

public class engine : actionController
{
    [Header("Movement Values")]
    public float moveSpeed;

    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementHandler(input.horAxis(), input.verAxis());
    }

    void movementHandler(float horAxis, float verAxis)
    {
        Vector2 newVel = new Vector2(horAxis * moveSpeed, verAxis * moveSpeed);
        transform.Translate(newVel * Time.deltaTime);
    }
}

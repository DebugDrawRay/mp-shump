using UnityEngine;
using System.Collections;

public class engine : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rigid;
    private controllerListener input;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        input = new controllerListener(GetComponent<playerShip>().playerNumber);
    }

    void Update()
    {
        movementHandler(input.horAxis(), input.verAxis());
    }

    void movementHandler(float horAxis, float verAxis)
    {
        Vector2 newVel = new Vector2(horAxis * moveSpeed, verAxis * moveSpeed);
        rigid.velocity = newVel;
    }
}

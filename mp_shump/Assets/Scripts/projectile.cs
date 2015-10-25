using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    public float speed;
    public float direction;

    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 xVel = (Vector2.right * direction) * speed;
        rigid.velocity = xVel;
    }
}

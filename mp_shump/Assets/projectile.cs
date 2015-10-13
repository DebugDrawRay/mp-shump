using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public int playerNumber;

    private Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 xVel = (Vector2.right * direction()) * speed;
        rigid.velocity = xVel;

        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    float direction()
    {
        float dir = 1;
        if(playerNumber == 2)
        {
            dir = -1;
        }
        return dir;
    }
}

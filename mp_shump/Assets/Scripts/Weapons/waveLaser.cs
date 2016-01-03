using UnityEngine;
using System.Collections;

public class waveLaser : projectile
{
    public float amp;
    public float cycles;
    public float horShift;

    private Vector2 origin;
    void Start()
    {
        origin = transform.position;
    }
    void Update()
    {
        float x = transform.position.x - origin.x;
        float y = amp * Mathf.Sin(((2 * Mathf.PI) / cycles) * x - horShift);
        Vector2 vel = transform.right;
        vel.y = y;
        rigid.velocity = (vel * projectileSpeed);

        checkIfVisible();
    }
}

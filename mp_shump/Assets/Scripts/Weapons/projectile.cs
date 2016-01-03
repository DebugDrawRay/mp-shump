using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    public float baseSpeed;
    protected float projectileSpeed;

    private Vector2 lastPosition;
    protected Rigidbody2D rigid;

    void Awake()
    {
        projectileSpeed = baseSpeed;
        rigid = GetComponent<Rigidbody2D>();
    }

    float calculateSpeed()
    {
        Vector2 newVel = Vector2.zero;
        Vector2 currentPosition = transform.root.position;
        if (lastPosition != currentPosition)
        {
            newVel = (lastPosition - currentPosition) / Time.deltaTime;
            lastPosition = currentPosition;
        }
        return newVel.magnitude;
    }

    protected void checkIfVisible()
    {
        if(!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }
}

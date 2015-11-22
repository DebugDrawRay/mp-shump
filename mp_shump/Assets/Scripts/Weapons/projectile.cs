using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    public float speed;
    protected Vector2 direction;
    protected int projectileNumber;
    protected Vector2 origin;
    protected Rigidbody2D rigid;
    protected float parentSpeed;

    protected delegate void updateFunctions();
    protected updateFunctions extraUpdate;
     
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        origin = transform.parent.position;
        direction = transform.parent.right;
        parentSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.parent = null;
        extraUpdate += checkIfVisible;
    }

    void checkIfVisible()
    {
        if(!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    bool calculateOffset()
    {
        float calc = projectileNumber % 2;
        if (calc == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

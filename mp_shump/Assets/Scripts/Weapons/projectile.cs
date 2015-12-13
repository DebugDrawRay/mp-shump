using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour
{
    public float speed;
    protected Vector2 direction;
    protected int projectileNumber;
    protected Vector2 origin;
    protected Rigidbody2D rigid;
    protected Vector2 parentVelocity;

    public void Init(Transform parent, Vector2 parentVelocity)
    {
        transform.SetParent(parent);
        this.parentVelocity = parentVelocity;
        rigid = GetComponent<Rigidbody2D>();
        origin = transform.parent.position;
        direction = transform.parent.right;
        transform.parent = null;
    }

    protected void checkIfVisible()
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

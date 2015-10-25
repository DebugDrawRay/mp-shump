using UnityEngine;
using System.Collections;

public class actionController : MonoBehaviour
{
    protected bool isActive
    {
        get;
        private set;
    }

    protected IinputListener input;
    protected GameObject parentObject;
    protected GameObject enemyObject;
    protected float facingDirection = 0;
    protected Quaternion facingRotation;

    public void construct(IinputListener selectedInput, GameObject parent, GameObject enemy, float facing)
    {
        input = selectedInput;
        parentObject = parent;
        enemyObject = enemy;
        facingDirection = facing;

        if (facing < 0)
        {
            facingRotation = Quaternion.Euler(new Vector3(0, 0, 180));
        }
        else
        {
            facingRotation = Quaternion.identity;
        }
    }

    public void enable(bool enabled)
    {
        isActive = enabled;
    }
}

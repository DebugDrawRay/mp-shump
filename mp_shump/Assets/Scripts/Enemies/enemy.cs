using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    public bool activeState = false;
    public bool canBeDespawned = false;
    protected actionController[] availableActions;
    protected status currentStatus;

    void Awake()
    {
        availableActions = GetComponents<actionController>();
        currentStatus = GetComponent<status>();
    }

    protected void checkActive()
    {
        if(GetComponent<Renderer>().isVisible)
        {
            activeState = true;
        }
        if(!GetComponent<Renderer>().isVisible && canBeDespawned)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    public bool activeState = false;
    public actionController[] availableActions;
    protected status currentStatus;

    void Awake()
    {
        availableActions = GetComponents<actionController>();
        currentStatus = GetComponent<status>();
    }

    void OnTriggerEnter2D(Collider2D hit)
    {

        if(hit.tag == "Camera")
        {
            activeState = true;
        }
    }
}
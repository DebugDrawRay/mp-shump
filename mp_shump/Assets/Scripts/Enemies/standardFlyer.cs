using UnityEngine;
using System.Collections;
public class standardFlyer : enemy
{
    void Start()
    {
        availableActions = GetComponents<actionController>();
    }

    void Update()
    {
        checkActive();

        foreach (actionController action in availableActions)
        {
            action.isActive = activeState;
        }

        if (currentStatus)
        {
            if(currentStatus.destroyed)
            {
                Destroy(gameObject);
            }
        }

        else
        {
            Debug.LogWarning("No Status Component Attached, Intentional?");
        }
    }
}

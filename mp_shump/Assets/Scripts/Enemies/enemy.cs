using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    public actionController[] availableActions;
    protected status currentStatus;

    void Awake()
    {
        currentStatus = GetComponent<status>();
    }
}
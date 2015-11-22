using UnityEngine;
using System.Collections;

public class actionController : MonoBehaviour
{
    public bool isActive
    {
        get;
        set;
    }

    public IinputListener input
    {
        get;
        set;
    }
}

using UnityEngine;
using System.Collections;

public class SceneStatic : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class levelAsset : ScriptableObject
{
    [SerializeField]
    public List<levelObject> levelObjects;
}

[Serializable]
public class levelObject
{
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public Vector2 prefabPos;

    public levelObject(GameObject objectPrefab, Vector2 objectPosition)
    {
        prefab = objectPrefab;
        prefabPos = new Vector2(Mathf.Abs(objectPosition.x), objectPosition.y);
    }
}

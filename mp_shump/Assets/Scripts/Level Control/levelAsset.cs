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
    [SerializeField]
    public AnimationCurve prefabCurve;
    [SerializeField]
    public float prefabSpeed;
    [SerializeField]
    public float prefabTimeScale;

    public levelObject(GameObject objectPrefab, Vector2 objectPosition, AnimationCurve objectCurve, float objectSpeed, float objectTimeScale)
    {
        prefab = objectPrefab;
        prefabPos = new Vector2(Mathf.Abs(objectPosition.x), objectPosition.y);
        prefabCurve = objectCurve;
        prefabSpeed = objectSpeed;
        prefabTimeScale = objectTimeScale;
    }
}

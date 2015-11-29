using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelFactory : MonoBehaviour
{
    public levelAsset levelToBuild;
    public bool createLevel = true;
    void Awake()
    {
        if (levelToBuild && createLevel)
        {
            buildLevel(levelToBuild);
        }   
    }

    public bool buildLevel(levelAsset levelToBuild)
    {
        List<levelObject> objs = levelToBuild.levelObjects;
        for(int i = 0; i < objs.Count; i++)
        {
            Vector2 pos = objs[i].prefabPos;

            GameObject left = Instantiate(objs[i].prefab) as GameObject;
            left.transform.position = new Vector2(pos.x * -1, pos.y);
            left.transform.rotation = Quaternion.Euler(0, 180, 0);

            GameObject right = Instantiate(objs[i].prefab) as GameObject;
            right.transform.position = pos;
            
        }
        return true;
    }

    void Start()
    {

    }
}

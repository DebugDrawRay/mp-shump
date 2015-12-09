using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelFactory : MonoBehaviour
{
    public levelAsset levelToBuild;
    public bool createLevel = true;

    public static levelFactory instance
    {
        get;
        private set;
    }

    void Awake()
    {
        initializeInstance();
    }

    void initializeInstance()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public bool buildLevel()
    {
        if (levelToBuild && createLevel)
        {
            List<levelObject> objs = levelToBuild.levelObjects;
            for (int i = 0; i < objs.Count; i++)
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
        else
        {
            Debug.LogError("No valid level to build");
            return false;
        }
    }

    void Start()
    {

    }
}

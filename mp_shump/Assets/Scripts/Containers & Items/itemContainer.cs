using UnityEngine;
using System.Collections;

public class itemContainer : MonoBehaviour
{
    public GameObject[] items;

    [Range(0.0f, 1.0f)]
    public float dropChance = 0.5f;

    private status currentStatus;

    void Awake()
    {
        currentStatus = GetComponent<status>();
        if(!currentStatus)
        {
            Debug.LogError("No Status Component Attached");
        }
    }

    void Update()
    {
        dropItems(currentStatus.destroyed);
    }

    void dropItems(bool trigger)
    {
        if(trigger && willDrop())
        {
            for(int i = 0; i < items.Length; i++)
            {
                Instantiate(items[i], transform.position, Quaternion.identity);
            }
            items = new GameObject[0];
        }
    }

    bool willDrop()
    {
        float chance = Random.Range(0f, 1f);
        if(chance <= dropChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

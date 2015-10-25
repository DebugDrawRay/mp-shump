using UnityEngine;
using System.Collections;

public class enviromentDestructable : MonoBehaviour
{
    [Header("Properties")]
    public GameObject[] debris;
    public int baseHealth;

    [Header("Valid Collisions")]
    public string[] hostiles;

    void Update()
    {
        checkStatus();
    }

    void checkStatus()
    {
        if(baseHealth <= 0)
        {
            destroySelf();
        }
    }

    void changeStatus(interactionSource source)
    {
        baseHealth -= source.damage;
    }

    void destroySelf()
    {
        if(debris.Length >= 0)
        {
            foreach(GameObject obj in debris)
            {
                Vector2 randomPos = Random.insideUnitCircle;
                Vector2 origin = transform.position;
                Vector2 spawn = randomPos + origin;
                Instantiate(obj, spawn, Quaternion.identity);
            }
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        foreach(string tag in hostiles)
        {
            if(hit.tag == tag)
            {
                changeStatus(hit.gameObject.GetComponent<interactionSource>());
            }
        }
    }
}

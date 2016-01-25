using UnityEngine;
using System.Collections;

public class interactionSource : MonoBehaviour
{
    [Header("Penalites")]
    public int damage;

    [Header("Upgrades")]
    public int weaponLevelIncrease;

    [Header("Object Status")]
    public bool isInvul;
    public bool persistent;
    public float lifetime;
    public GameObject deathAnim;

    [Header("Collisions")]
    public string[] solidObjects;

    void Update()
    {
        if(!isInvul)
        {
            runLife();
        }
    }
    void runLife()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        foreach(string tag in solidObjects)
        {
            if(hit.tag == tag)
            {
                deathEvent();
            }
        }
    }

    public void deathEvent()
    {
        if (!persistent)
        {
            if (deathAnim)
            {
                Instantiate(deathAnim, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

}

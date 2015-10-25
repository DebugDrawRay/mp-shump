using UnityEngine;
using System.Collections;

public class interactionSource : MonoBehaviour
{
    public int damage;
    public bool isInvul;
    public float lifetime;
    public string[] solidObjects;
    public GameObject deathAnim;
    public GameObject parent;

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
        if (!isInvul && hit.gameObject != parent)
        {
            deathEvent();
        }
    }

    void deathEvent()
    {
        if (deathAnim)
        {
            Instantiate(deathAnim, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }

}

using UnityEngine;
using System.Collections;


public class status : MonoBehaviour
{
    public float baseHealth = 10;
    public float currentHealth
    {
        get;
        private set;
    }

    public int lives = 1;

    public bool destroyed;
    public GameObject deathAnim;

    public string[] excludedTags;

    void Awake()
    {
        currentHealth = baseHealth;
    }

    void Update()
    {
        checkDamageState();
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        interactionSource interact = hit.GetComponent<interactionSource>();
        if(interact)
        {
            if(checkIfHostile(interact.gameObject.tag))
            {
                hostileInteraction(interact);
            }
        }
    }

    bool checkIfHostile(string tag)
    {
        foreach(string excluded in excludedTags)
        {
            if(tag == excluded)
            {
                return false;
            }
        }
        if (tag == this.tag)
        {
            return false;
        }
        return true;
    }
    void hostileInteraction(interactionSource hostile)
    {
        currentHealth -= hostile.damage;
        hostile.deathEvent();
    }
    void checkDamageState()
    {
        if(currentHealth <= 0)
        {
            if(lives > 0)
            {
                lives--;
                respawn();
            }
            else
            {
                destroyed = true;
                lives = 0;
                Instantiate(deathAnim, transform.position, Quaternion.identity);
            }
        }
    }

    void respawn()
    {
        currentHealth = baseHealth;
        Debug.Log("Respawn");
    }
}

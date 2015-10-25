using UnityEngine;
using System.Collections;


public class status : MonoBehaviour
{
    public float health = 10;
    public float baseHealth;

    public float shields = 10;
    public float baseShields;

    public int lives = 1;

    public bool hit;
    public bool destroyed;
    public GameObject deathAnim;

    public string[] hostileObjectTags;

    void Awake()
    {
        baseHealth = health;
        baseShields = shields;
    }

    void Update()
    {
        checkDamageState();
        if(hit == true)
        {
            hit = false;
        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("hit");
        interactionSource hostile = hit.GetComponent<interactionSource>();
        if(hostile)
        {
            if(checkIfHostile(hostile.gameObject.tag))
            {
                hostileInteraction(hostile);
            }
        }
    }

    bool checkIfHostile(string tag)
    {
        foreach(string hostile in hostileObjectTags)
        {
            if(tag == hostile)
            {
                return true;
            }
        }
        return false;
    }
    void hostileInteraction(interactionSource hostile)
    {
        if (hostile.parent != this.gameObject)
        {
            health -= hostile.damage;
            hit = true;
        }
    }
    void checkDamageState()
    {
        if(health <= 0)
        {
            respawn();            
        }
        if(lives < 0)
        {
            lives = 0;
            destroyed = true;
            Debug.Log("Destroyed");
        }
    }

    void respawn()
    {
        lives--;
        health = baseHealth;
        Debug.Log("Respawn");
    }
}

using UnityEngine;
using System.Collections;
using System;

public class status : MonoBehaviour, IInformationBroadcast 
{
    public PlayerInformationController targetInformationUi
    {
        get;
        set;
    }
    [Header("Health and Lives")]
    public float baseHealth = 10;
    public float currentHealth
    {
        get;
        private set;
    }
    public int lives = 1;

    public bool destroyed
    {
        get;
        private set;
    }

    public bool respawning
    {
        get;
        private set;
    }

    public GameObject deathAnim;

    [Header("Weapons")]
    public int currentWeaponLevel;

    [Header("Collisions")]
    public string[] excludedTags;

    private bool setLives;

    void Awake()
    {
        currentHealth = baseHealth;
    }

    void Start()
    {
        //targetInformationUi.ChangeCurrentLives(lives);
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
            if(!checkIfExcluded(interact.gameObject.tag))
            {
                interaction(interact);
            }
        }
    }

    bool checkIfExcluded(string tag)
    {
        foreach(string excluded in excludedTags)
        {
            if(tag == excluded)
            {
                return true;
            }
        }
        if (tag == this.tag)
        {
            return true;
        }
        return false;
    }
    void interaction(interactionSource interactable)
    {
        currentHealth -= interactable.damage;
        currentWeaponLevel += interactable.weaponLevelIncrease;

        if(interactable.damage > 0 && currentWeaponLevel > 0)
        {
            currentWeaponLevel--;
        }
        interactable.deathEvent();
    }

    void checkDamageState()
    {
        if(currentHealth <= 0)
        {
            if(lives > 0)
            {
                targetInformationUi.ChangeCurrentLives(-1);
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
        else
        {
            respawning = false;
        }

    }

    void respawn()
    {
        respawning = true;
        Instantiate(deathAnim, transform.position, Quaternion.identity);
        currentHealth = baseHealth;
        Debug.Log("Respawn");
    }
}
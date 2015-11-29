using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class missile : projectile
{
    public float timeToFire;
    private bool fire = false;

    public GameObject enemyObject;

    public float rotSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.parent = null;
        timeToFire -= Time.deltaTime;
        if(timeToFire <= 0)
        {
            fire = true;
        }

        if(fire)
        {
            if (currentTarget())
            {
                float deltay = currentTarget().transform.position.y - transform.position.y;
                float deltax = currentTarget().transform.position.x - transform.position.x;
                float angle = Mathf.Atan2(deltay, deltax) * 180 / Mathf.PI;
                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));
                Quaternion newRot = Quaternion.Slerp(transform.rotation, rot, rotSpeed);
                transform.rotation = newRot;
            }
            rigid.velocity = transform.right * speed;
        }

        extraUpdate();
    }

    GameObject currentTarget()
    {
        GameObject closestTarget = null;
        float shortestDistance = 500;
        GameObject[] targets = getEnemyTargets();
        for (int i = 0; i < targets.Length; ++i)
        {
            GameObject target = targets[i];
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if(distance < shortestDistance && target.GetComponent<Renderer>().isVisible)
            {
                shortestDistance = distance;
                closestTarget = target;
            }
        }
        return closestTarget;
    }

    GameObject[] getEnemyTargets()
    {
        GameObject[] sceneEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemies = new GameObject[sceneEnemies.Length + 1];
        for(int i = 0; i < enemies.Length - 1; i++)
        {
            enemies[i] = sceneEnemies[i];
        }
        enemies[enemies.Length - 1] = getEnemyPlayer();
        return enemies;
    }
    GameObject getEnemyPlayer()
    {
        if(tag == "Player1")
        {
            return GameObject.FindGameObjectWithTag("Player2");
        }
        if (tag == "Player2")
        {
            return GameObject.FindGameObjectWithTag("Player1");
        }
        return null;
    }
}

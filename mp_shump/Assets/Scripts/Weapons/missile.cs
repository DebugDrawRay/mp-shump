using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class missile : projectile
{
    public float timeToFire;
    private bool fire = false;

    public GameObject enemyObject;

    public Vector2[] launchDirections;
    public float launchSpeed;
    public float launchRotation;

    public float launchDelay;
    private float currentLaunchDelay;

    public float rotSpeed;
    public float falloffDistance;
    private bool lostTarget = false;

    public float startSpeed;
    public float maxSpeed;
    public float acceleration;
    private float currentSpeed;

    void Start()
    {
        currentSpeed = startSpeed;
        currentLaunchDelay = launchDelay;
        launchMissiles();
    }
    void Update()
    {
        checkIfVisible();
        launchDelay -= Time.deltaTime;
        if(launchDelay <= 0)
        {
            fireMissile();
        }
    }

    void launchMissiles()
    {
        float mod = 1;
        for (int i = 0; i <= launchDirections.Length - 1; i++)
        {
            float yRot = transform.rotation.eulerAngles.y;
            Quaternion initRot = Quaternion.Euler(0, yRot, launchRotation * mod);
            transform.rotation = initRot;
            rigid.AddForce(launchDirections[i] * launchSpeed);
            mod = -mod;
        }
    }

    void fireMissile()
    {
        GameObject target = currentTarget();

        if (target)
        {
            float deltay = target.transform.position.y - transform.position.y;
            float deltax = target.transform.position.x - transform.position.x;
            float angle = Mathf.Atan2(deltay, deltax) * 180 / Mathf.PI;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle));
            Quaternion newRot = Quaternion.Slerp(transform.rotation, rot, rotSpeed);

            float dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist > falloffDistance && !lostTarget)
            {
                transform.rotation = newRot;
            }
            else
            {
                lostTarget = true;
            }
        }
        rigid.velocity = transform.right * flightSpeed();
    }

    float flightSpeed()
    {
        if(currentSpeed <= maxSpeed - .01)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration);
        }
        return currentSpeed;
    }
    GameObject currentTarget()
    {
        GameObject closestTarget = null;
        float shortestDistance = 25;
        GameObject[] targets = getEnemyTargets();
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; ++i)
            {
                GameObject target = targets[i];
                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance < shortestDistance && target.GetComponent<Renderer>().isVisible)
                {
                    shortestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        return closestTarget;
    }

    GameObject[] getEnemyTargets()
    {
        GameObject[] sceneEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (getEnemyPlayer() != null)
        {
            GameObject[] enemies = new GameObject[sceneEnemies.Length + 1];
            for (int i = 0; i < enemies.Length - 1; i++)
            {
                enemies[i] = sceneEnemies[i];
            }
            enemies[enemies.Length - 1] = getEnemyPlayer();
            return enemies;
        }
        else
        {
            return sceneEnemies;
        }
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

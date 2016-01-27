using UnityEngine;
using System.Collections;

public class enviromentDestructable : MonoBehaviour
{
    [Header("Properties")]
    public GameObject[] debris;
    public float explosionForce;
    private status currentStatus;
    private bool canBeDespawned = false;

    void Awake()
    {
        currentStatus = GetComponent<status>();
    }

    void Update()
    {
        if(currentStatus)
        {
            if(currentStatus.destroyed)
            {
                destroySelf();
            }
        }
        else
        {
            Debug.LogError("No status component attached, intentional?");
        }
        checkActive();
    }

    void checkActive()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            canBeDespawned = true;
        }
        if (!GetComponent<Renderer>().isVisible && canBeDespawned)
        {
            Destroy(gameObject);
        }
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
                GameObject newObj = Instantiate(obj, spawn, Quaternion.identity) as GameObject;
                newObj.GetComponent<Rigidbody2D>().AddForce(randomPos * explosionForce);
            }
        }
        Destroy(this.gameObject);
    }
}

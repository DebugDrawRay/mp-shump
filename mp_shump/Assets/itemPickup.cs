using UnityEngine;
using System.Collections;

public class itemPickup : MonoBehaviour
{
    [Header("Status")]
    public int lives;

    void OnTriggerEnter2D(Collider2D hit)
    {
        status hasStatus = hit.GetComponent<status>();

        if(hasStatus)
        {
            hasStatus.lives += lives;
            Destroy(gameObject);
        }
    }
}

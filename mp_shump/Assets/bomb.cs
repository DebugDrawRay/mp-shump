using UnityEngine;
using System.Collections;

public class bomb : projectile
{
    [Header("Properties")]
    public float detonationTimer;
    public float blastRadius;
    public float radiusExpandRate;

    [Header("Visuals")]
    public GameObject blastAnim;

    void Update()
    {
        if(detonationTimer > 0)
        {
            detonationTimer -= Time.deltaTime;
        }
        else
        {
            explode();
        }
    }

    void explode()
    {
        Vector2 newScale = new Vector2(blastRadius, blastRadius);
        transform.localScale = Vector2.Lerp(transform.localScale, newScale, radiusExpandRate);
    }
}

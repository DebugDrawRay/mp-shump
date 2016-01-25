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

    private bool explodeBomb;

    void Start()
    {
        rigid.AddForce(transform.right * projectileSpeed);
    }
    void Update()
    {
        if(detonationTimer > 0)
        {
            detonationTimer -= Time.deltaTime;
        }
        else
        {
            explodeBomb = true;
        }

        if(explodeBomb)
        {
            explode();
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag != gameObject.tag)
        {
            explodeBomb = true;
        }

        projectile isProjectile = hit.GetComponent<projectile>();
        if(isProjectile)
        {
            Destroy(hit.gameObject);
        }
    }
    void explode()
    {
        Vector2 newScale = new Vector2(blastRadius, blastRadius);
        transform.localScale = Vector2.Lerp(transform.localScale, newScale, radiusExpandRate);
        rigid.velocity = Vector2.zero;
        GetComponent<AudioSource>().enabled = true;
    }
}

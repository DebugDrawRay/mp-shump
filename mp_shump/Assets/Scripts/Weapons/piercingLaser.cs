using UnityEngine;
using System.Collections;

public class piercingLaser : MonoBehaviour
{
    public GameObject projectile;
    public float length;
    public float spawnDelay;

    private Vector2 parentVel;

    public static piercingLaser instance
    {
        get;
        private set;
    }

    void initializeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Awake()
    {
        parentVel = GetComponent<Rigidbody2D>().velocity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        initializeInstance();
    }

    void Start()
    {
        StartCoroutine(fireLasers());
    }

    IEnumerator fireLasers()
    {
        for (int i = 1; i <= length; i++)
        {
            spawnLaser(i);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    
    void spawnLaser(int i)
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        proj.transform.SetParent(transform.parent);
        proj.tag = transform.parent.tag;
        proj.GetComponent<Rigidbody2D>().velocity = parentVel;
        if(i == length)
        {
            Destroy(gameObject);
        }
    }
}

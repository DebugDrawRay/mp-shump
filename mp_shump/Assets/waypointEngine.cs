using UnityEngine;
using System.Collections;

public class waypointEngine : actionController
{
    [Header("Waypoints")]
    public waypointSet setToUse;
    public float reachedPositionBuffer;

    [Header("Movement")]
    public float speed;
    public float interpolation;

    private Rigidbody2D rigid;

    private Vector2 currentVector;
    private int currentWaypoint;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        createPath();
    }

    void createPath()
    {

    }
    void Update()
    {
        if(isActive)
        {
            runEngine();
        }
    }

    void runEngine()
    {
        Vector2 wp = setToUse.waypoints[currentWaypoint];
        if (currentWaypoint < setToUse.waypoints.Length)
        {

            Vector2 origin = transform.position;
            Vector2 newVector = wp - origin;
            currentVector = Vector2.Lerp(currentVector, newVector.normalized, interpolation);
        }
        else
        {
            currentVector = transform.forward;
        }
        rigid.velocity = currentVector * speed;
        if (Vector2.Distance(transform.position, wp) <= reachedPositionBuffer)
        {

        }
    }
}

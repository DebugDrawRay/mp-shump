using UnityEngine;
using System.Collections;

public class patternEngine : actionController
{
    public AnimationCurve pattern;

    public float speed;
    public float timeScale;
    private float currentTime;

    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (isActive)
        {
            currentTime += Time.deltaTime * timeScale;
            Vector2 movement = transform.right;
            movement.y = pattern.Evaluate(currentTime);

            rigid.velocity = movement * speed;
        }
    }
}

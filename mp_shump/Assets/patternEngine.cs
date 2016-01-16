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
        currentTime += Time.deltaTime * timeScale;
        Vector2 movement = transform.right;
        movement.y = pattern.Evaluate(currentTime);

        rigid.velocity = movement * speed;
    }
    /*[System.Serializable]
    public class movementVector
    {
        public Vector2 direction;
        public float time;
        public movementVector(Vector2 _direction, float _time)
        {
            direction = _direction;
            time = _time;
        }
    }

    public movementVector[] vectorSets;
    public float speed;
    public float vectorSmooth;
    public bool looping;

    private int currentIndex;
    private Vector2 currentVector;
    private Vector2 targetVector;
    private float currentTime;

    private Rigidbody2D rigid;

    void Start()
    {
        currentTime = vectorSets[currentIndex].time;
        rigid = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (isActive)
        {
            engine();
        }
    }
    void engine()
    {
        targetVector = vectorSets[currentIndex].direction;
        targetVector.x = targetVector.x * (transform.right.x / transform.right.x);

        currentVector.x = targetVector.x;
        currentVector.y = Mathf.Lerp(currentVector.y, targetVector.y, vectorSmooth);

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentIndex++;
            currentVector = targetVector;
            if (currentIndex >= vectorSets.Length)
            {
                if (looping)
                {
                    currentIndex = 0;
                }
                else
                {
                    currentVector = transform.forward;
                }
            }           
            currentTime = vectorSets[currentIndex].time;
        }

        rigid.velocity = currentVector * speed;
    }*/
}

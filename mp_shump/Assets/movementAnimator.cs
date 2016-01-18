using UnityEngine;
using System.Collections;

public class movementAnimator : actionController
{
    private enum lean
    {
        top,
        topside,
        side,
        bottomside,
        bottom
    }

    private lean currentLean;

    private float currentLeanAmount;

    public Sprite[] movementSprites;

    private SpriteRenderer render;
    private Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        checkLean();
    }

    void checkLean()
    {
        currentLeanAmount = input.verAxis();
        Debug.Log(currentLeanAmount);
        if (currentLeanAmount >= 1)
        {
            currentLean = lean.top;
        }
        else if (currentLeanAmount >= .5)
        {
            currentLean = lean.topside;
        }
        else if (currentLeanAmount <= -1)
        {
            currentLean = lean.bottom;
        }
        else if (currentLeanAmount <= -.5)
        {
            currentLean = lean.bottomside;
        }
        else if (currentLeanAmount < .5 && currentLeanAmount > -.5)
        {
            currentLean = lean.side;
        }

        switch (currentLean)
        {
            case lean.top:
                render.sprite = movementSprites[0];
                break;
            case lean.topside:
                render.sprite = movementSprites[1];
                break;
            case lean.side:
                render.sprite = movementSprites[2];
                break;
            case lean.bottomside:
                render.sprite = movementSprites[3];
                break;
            case lean.bottom:
                render.sprite = movementSprites[4];
                break;
        }
    }
}

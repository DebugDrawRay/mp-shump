using UnityEngine;
using System.Collections;

public class triggerRotation : MonoBehaviour
{
    private gameController controller;
    private RectTransform localRect;
    public gameController.gameState triggerState;

    public Vector2 anchorMin;
    public Vector2 anchorMax;
    public Vector2 position;
    public float zRotation;
    public float rotationSpeed;

    private bool setAnchors;
    void Start()
    {
        localRect = GetComponent<RectTransform>();
        controller = gameController.instance;
    }

    void Update()
    {
        if(controller && controller.currentState == triggerState)
        {
            localRect.anchorMin = Vector2.Lerp(localRect.anchorMin, anchorMin, rotationSpeed);
            localRect.anchorMax = Vector2.Lerp(localRect.anchorMax, anchorMax, rotationSpeed);

            localRect.anchoredPosition = Vector2.Lerp(localRect.anchoredPosition, position, rotationSpeed);
            Vector3 newRot = new Vector3(0, 0, zRotation);
            localRect.rotation = Quaternion.Lerp(localRect.rotation, Quaternion.Euler(newRot), rotationSpeed);
        }
    }
}

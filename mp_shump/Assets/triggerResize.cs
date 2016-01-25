using UnityEngine;
using System.Collections;

public class triggerResize : MonoBehaviour
{
    private gameController controller;

    public Rect resizeTo;
    public float resizeSpeed;

    private Camera localCamera;
    
    public gameController.gameState triggerState;
    void Start()
    {
        localCamera = GetComponent<Camera>();
        controller = gameController.instance;
        if(localCamera.rect.position.y == 0f)
        {
            resizeTo.position = new Vector2(0.5f, resizeTo.position.y);
        }
    }

    void Update()
    {
        if(controller.currentState == triggerState)
        {
            Vector2 position = Vector2.Lerp(localCamera.rect.position, resizeTo.position, resizeSpeed);
            Vector2 size = Vector2.Lerp(localCamera.rect.size, resizeTo.size, resizeSpeed);
            localCamera.rect = new Rect(position, size);
        }
    }
}

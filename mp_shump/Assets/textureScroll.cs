using UnityEngine;
using System.Collections;

public class textureScroll : MonoBehaviour
{
    
    public float uvAnimationRate;
    public float uvYPos;

    private int materialIndex = 0;
    private string textureName = "_MainTex";

    private Vector2 uvOffset;
    private Renderer render;

    void Awake()
    {
        uvOffset = new Vector2(0, uvYPos);
        render = GetComponent<Renderer>();
    }
    void Update()
    {
        uvOffset.x += uvAnimationRate * Time.deltaTime;
        if (render.enabled)
        {
            render.material.SetTextureOffset(textureName, uvOffset);
        }
    }
}

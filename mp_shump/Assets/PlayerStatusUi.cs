using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusUi : MonoBehaviour
{
    private RectTransform localRect;
    private Camera cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        localRect = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player1").transform.position);
        localRect.position = pos;
    }
}
